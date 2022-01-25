using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;
using ELibrary.Infrastructure.Persistence.Integrations.Email;
using ELibrary.Infrastructure.Persistence.Integrations.Paystack;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Persistence.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly LibraryContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IPaystackService _paystackService;
        private readonly IMailSender _mailSender;
        private readonly IUserRepository _userRepository;


        public PaymentService(LibraryContext context, UserManager<User> userManager, IPaystackService paystackService, IMailSender mailSender, IUserRepository userRepository)
        {
            _context = context;
            _paystackService = paystackService;
            _mailSender = mailSender;
            _userManager = userManager;
            _userRepository = userRepository;
        }

        public async Task<Result> CheckoutAsync(Checkout request)
        {
            if (!request.CartItems.Any())
            {
                throw new BadRequestException(ResponseMessages.EmptyCartError);
            }
            var userr = await _userRepository.GetByEmail(request.Email);
           
            var customerExists = userr != null;
            User user = null;

            if (!customerExists)
            {
                if (string.IsNullOrEmpty(request.FullName) || string.IsNullOrEmpty(request.Email))
                {
                    throw new BadRequestException("FullName and Email is required");
                }
                var userEmailExists = await _context.Users.AnyAsync(m => m.Email == request.Email);
                if (userEmailExists)
                {
                    throw new BadRequestException(ResponseMessages.EmailAlreadyExistError);
                }
                user = new User
                {
                    Email = request.Email,
                    LastName = request.FullName,
                    FirstName = request.FullName,
                    UserType = UserType.LibraryUser,
                    VerificationStatus = VerificationStatus.NotVerified
                };
                await _userRepository.AddAsync(user);
                await _userRepository.SaveChangesAsync();

                
            }
            else
            {
                user = userr;
            }

            var transaction = new BookTransaction()
            {
                UserId = user.Id,
                Discount = 0M,
                TransactionStatus = TransactionStatus.Initialize,
                User = user,
                AmountPaid = 0M,
                TransactionReference = $"elib_{Guid.NewGuid().ToString().Substring(0, 8)}",
            };
            var cart = request.CartItems.Where(c => c.Quantity > 0).ToDictionary(c => c.Id, c => c.Quantity);

            var books = await _context.Books
                .Where(c => cart.Keys.Contains(c.Id)).ToListAsync();
            var invoice = new TransactionInvoice
            {
                CustomerEmail = user.Email,
                CustomerName = $"{user.FirstName} {user.LastName}",
                TransactionStatus = transaction.TransactionStatus,
                TransactionReference = transaction.TransactionReference,
                TransactionId = transaction.Id
            };

            var bookIds = cart.Keys.ToList();
            //var merchantSellingRates = await GetMerchantSellingRates(merchant.Id, bookIds);

            foreach (var book in books)
            {
                var quantity = cart[book.Id];
                //var rate = cart[book.Price];
                var rate = book.Price;
               /* merchantSellingRates.TryGetValue(book.Id, out var rate);
                if (rate < book.Rate)
                {
                    rate = cardPinType.Rate + 50;
                }*/

                var bookTransactionItem = new BookTransactionItem
                {
                    
                    Book = book,
                    BookId = book.Id,
                    BookTransaction = transaction,
                    BookTransactionId = transaction.Id,
                    Rate = rate,
                    Quantity = quantity,
                    Discount = 0M
                };
                transaction.BookTransactionItems.Add(bookTransactionItem);
                transaction.Amount += bookTransactionItem.Rate * bookTransactionItem.Quantity;
                invoice.InvoiceItems.Add(new InvoiceItem
                {
                    BookTitle = book.Title,
                    Quantity = bookTransactionItem.Quantity,
                    Rate = bookTransactionItem.Rate,
                    BookId = book.Id,
                    BookImage = book.BookPDF
                });
                invoice.TransactionAmount = transaction.Amount;
                _context.Entry(bookTransactionItem).State = EntityState.Added;

            }

            try
            {
                var providerResponse = await _paystackService.InitializeTransaction(transaction.TransactionReference, user.Email, transaction.Amount);
                invoice.AuthorizationUrl = providerResponse.Data.AuthorizationUrl;
            }
            catch (HttpRequestException)
            {
                _context.Entry(transaction).State = EntityState.Added;
                await _context.SaveChangesAsync();
                throw new BadGatewayException(ResponseMessages.PaymentProviderError);
            }
            _context.Entry(transaction).State = EntityState.Added;
            await _context.SaveChangesAsync();

            if (!customerExists)
            {
                var baseResetLink = "https://localhost:44307/auth/verify";
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var activationLink = $"{baseResetLink}?id={user.Id}&token={HttpUtility.UrlEncode(token)}";

                await _mailSender.SendWelcomeMail(user.Email, $"{ user.FirstName} {user.LastName}", activationLink);
            }

            await _mailSender.SendTransactionInvoiceMail(user.Email, user.FirstName, MapInvoice(invoice));
            return new Result
            {
                Message = ResponseMessages.Successful,
                Status = true,
                Data = invoice
            };


        }
        private TransactionInfo MapInvoice(TransactionInvoice invoice)
        {
            return new TransactionInfo
            {
                AuthorizationUrl = invoice.AuthorizationUrl,
                TransactionReference = invoice.TransactionReference,
                
                TransactionAmount = invoice.TransactionAmount,
               
                UserEmail = invoice.CustomerEmail,
                UserName = invoice.CustomerName,
               
                TransactionStatus = invoice.TransactionStatus,
                
                TransactionId = invoice.TransactionId,
                TransactionDate = DateTime.UtcNow,
                TransactionItems = invoice.InvoiceItems.Select(i => new TransactionItem()
                {
                    BookId = i.BookId,
                    BookImage = i.BookImage,
                    BookTitle = i.BookTitle,
                    Quantity = i.Quantity,
                    Rate = i.Rate
                }).ToList()
            };
        }
    }
}
