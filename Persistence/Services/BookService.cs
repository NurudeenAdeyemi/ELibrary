using Domain.DTOs;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IUserRepository _userRepository;
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository, IUserRepository userRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse> AddBook(CreateBookRequestModel model)
        {

            var bookExist = await _bookRepository.ExistsAsync(b => b.Title == model.Title);

            if (bookExist)
            {
                throw new BadRequestException($"Book with {model.Title} already exist");
            }
            var book = new Book

            {
                ISBN = model.ISBN,
                Language = model.Language,
                BookPDF = model.BookPDF,
                NumberOfPages = model.NumberOfPages,
                BookImage = model.BookImage,
                PublicationDate = model.PublicationDate,
                Publisher = model.Publisher,
                Price = model.Price,
                AvailabilityStatus = model.AvailabilityStatus,
                AccessibilityStatus = model.AccessibilityStatus,
                Subject = model.Subject,
                Title = model.Title

            };

            var authors = await _authorRepository.GetSelectedAuthors(model.Authors);
            foreach (var author in authors)
            {
                var bookAuthor = new BookAuthor
                {
                    Book = book,
                    BookId = book.Id,
                    Author = author,
                    AuthorId = author.Id
                };

                book.BookAuthors.Add(bookAuthor);
            }

            var categories =await _categoryRepository.GetSelectedCategories(model.Categories);
            foreach (var category in categories)
            {
                var bookCategory = new BookCategory
                {
                    Book = book,
                    BookId = book.Id,
                    Category = category,
                    CategoryId = category.Id
                };
                book.BookCategories.Add(bookCategory);
            }

           await _bookRepository.AddAsync(book);
            await _authorRepository.SaveChangesAsync();
            return new BaseResponse
            {
                Status = true,
                Message = "Book successfully added"
            };
        }

        public int BooksBorrowed(int userId)
        {
            return _bookRepository.NumberOfBooksBorrowed(userId);
        }

        public async Task<BaseResponse> CheckOutBook(CheckOutBookRequestModel model)
        {
            var user = await _userRepository.GetAsync(model.UserId);
            if (user == null)
            {
                throw new NotFoundException($"user with {model.UserId} does not exist");
            }
            var book = await _bookRepository.GetAsync(model.BookId);
            if (book == null)
            {
                throw new NotFoundException($"book with Id: {model.BookId} does not exist");
            }
            var userBorrowedBooks = await _bookRepository.GetListOfBooks(user.Id);
            var bookBorrowedExist = userBorrowedBooks.Any(p => p.Id == book.Id);
            if (bookBorrowedExist)
            {
                throw new BadRequestException($"Book with title: {book.Title} already borrowed by user");
            }
            if ( _bookRepository.NumberOfBooksBorrowed(user.Id) >= 5)
            {
                throw new BadRequestException($"Books Limit reached");
            }
            if (book.AvailabilityStatus != BookAvailabilityStatus.AVAILABLE)
            {
                throw new BadRequestException($"Book with title: {book.Title} is not available");
            }
            if (book.AccessibilityStatus != BookAccessibilityStatus.FREE)
            {
                throw new BadRequestException($"Book with title: {book.Title} is not free");
            }
            var bookLending = new BookLending
            {
                UserId = user.Id,
                BookId = book.Id,
                DueDate = DateTime.Today.AddDays(5)
            };
            await _bookRepository.CheckoutBookItem(bookLending);

            return new BaseResponse
            {
                
                Status = true,
                Message = $"Book with title: {book.Title} borrowed successfully"
            };
        }

        public async Task<BaseResponse> DeleteBook(int id)
        {
            var book = await _bookRepository.GetAsync(id);
            await _bookRepository.DeleteAsync(book);
            await _bookRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Book Deleted Successfully"
            };
        }

        public async Task<BookResponseModel> GetBook(int id)
        {
            var book = await _bookRepository.GetAsync(id);

            if (book == null)
            {
                throw new NotFoundException($"Book with id {id} not found");
            }
            return new BookResponseModel
            {
                Data = new BookDTO
                {
                    Id = book.Id,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Subject = book.Subject,
                    BookPDF = book.BookPDF,
                    Language = book.Language,
                    BookImage = book.BookImage,
                    NumberOfPages = book.NumberOfPages,
                    Price = book.Price,
                    AccessibilityStatus = book.AccessibilityStatus,
                    AvailabilityStatus = book.AvailabilityStatus,
                    Publisher = book.Publisher,
                    PublicationDate = book.PublicationDate,

                    Authors = book.BookAuthors.Select(a => new AuthorDTO()
                    {
                        Id = a.AuthorId,
                        FirstName = a.Author.FirstName,
                        LastName = a.Author.LastName,
                        Biography = a.Author.Biography
                    }).ToList(),
                    BookCategories = book.BookCategories.Select(a => new CategoryDTO()
                    {
                        Id = a.CategoryId,
                        Name = a.Category.Name,
                    }).ToList()
                    
                },
                Message = "Book Retrieved successfully",
                Status = true
               
            };
        }

        public async Task<BookResponseModel> GetBookByTitle(string title)
        {
            var book = await _bookRepository.GetBookByTitle(title);

            if (book == null)
            {
                throw new NotFoundException($"Book with title {title} not found");
            }
            return new BookResponseModel
            {
                Data = new BookDTO
                {
                    Title = book.Title,
                    ISBN = book.ISBN,
                    Subject = book.Subject,
                    BookPDF = book.BookPDF,
                    Language = book.Language,
                    BookImage = book.BookImage,
                    NumberOfPages = book.NumberOfPages,
                    Price = book.Price,
                    AccessibilityStatus = book.AccessibilityStatus,
                    AvailabilityStatus = book.AvailabilityStatus,
                    Publisher = book.Publisher,
                    PublicationDate = book.PublicationDate,
                    Authors = book.BookAuthors.Select(a => new AuthorDTO()
                    {
                        FirstName = a.Author.FirstName,
                        LastName = a.Author.LastName,
                        Biography = a.Author.Biography
                    }).ToList(),
                    BookCategories = book.BookCategories.Select(a => new CategoryDTO()
                    {
                        Id = a.CategoryId,
                        Name = a.Category.Name,
                    }).ToList()

                },
                Status = true,
                Message = "Book retrieved successfully"
         
            };
            
        }

        public async Task<BooksResponseModel> GetBooks()
        {
            var books = await _bookRepository.Query()
                .Include(b => b.BookCategories)
                .ThenInclude(c => c.Category)
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .Select(b => new BookDTO
            {
                Id = b.Id,
                Title = b.Title,
                ISBN = b.ISBN,
                Subject = b.Subject,
                BookPDF = b.BookPDF,
                Language = b.Language,
                BookImage = b.BookImage,
                NumberOfPages = b.NumberOfPages,
                Price = b.Price,
                AccessibilityStatus = b.AccessibilityStatus,
                AvailabilityStatus = b.AvailabilityStatus,
                Publisher = b.Publisher,
                PublicationDate = b.PublicationDate,
                Authors = b.BookAuthors.Select(a => new AuthorDTO()
                {
                    Id = a.Author.Id,
                    FirstName = a.Author.FirstName,
                    LastName = a.Author.LastName,
                    Biography = a.Author.Biography
                }).ToList(),
                BookCategories = b.BookCategories.Select(a => new CategoryDTO()
                {
                    Id = a.CategoryId,
                    Name = a.Category.Name,
                }).ToList()

            }).ToListAsync();

            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"
            };
        }

        public async Task<BooksResponseModel> GetBooksBorrowed(int userId)
        {
            var books = await _bookRepository.GetListOfBooks(userId);

            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BooksResponseModel> GetBooksByAccessibilityStatus(BookAccessibilityStatus accessibilityStatus)
        {
            var books = await _bookRepository.GetBooksByAccessibilityStatus(accessibilityStatus);

            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BooksResponseModel> GetBooksByAuthor(int authorId)
        {
            var books = await _bookRepository.GetBooksByAuthor(authorId);

            if (books.Count == 0)
            {
                throw new NotFoundException($"Book with author id {authorId} not found");
            }
            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BooksResponseModel> GetBooksByAvailabilityStatus(BookAvailabilityStatus availabilityStatus)
        {
            var books = await _bookRepository.GetBooksByAvailabilityStatus(availabilityStatus);

            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BooksResponseModel> GetBooksByCategory(int categoryId)
        {
            var books = await _bookRepository.GetBooksByCategory(categoryId);

            if (books.Count == 0)
            {
                throw new NotFoundException($"Book with category id {categoryId} not found");
            }
            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BooksResponseModel> GetBooksByPublicationDate(int publicationDate)
        {
            var books = await _bookRepository.GetBooksByPublicationDate(publicationDate);

            if (books.Count == 0)
            {
                throw new NotFoundException($"Book with publication year {publicationDate} not found");
            }
            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BooksResponseModel> GetBooksByPublisher(string publisher)
        {
            var books = await _bookRepository.GetBooksByPublisher(publisher);

            if (books.Count == 0)
            {
                throw new NotFoundException($"Books published by {publisher} not found");
            }
            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BaseResponse> UpdateBook(int id, UpdateBookRequestModel model)
        {
            var book = await _bookRepository.GetAsync(id);

            if (book == null)
            {
                throw new NotFoundException($"Book with id {id} not found");
            }

            book.Price = model.Price;
            book.BookImage = model.BookImage;
            book.BookPDF = model.BookPDF;
            book.AccessibilityStatus = model.AccessibilityStatus;
            book.AvailabilityStatus = model.AvailabilityStatus;

            await _bookRepository.UpdateAsync(book);
            await _bookRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "Book Successfully updated"
            };
        }
    }
}
