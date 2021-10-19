using Domain.DTOs;
using Domain.Enums;
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
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<BaseResponse> AddBook(CreateBookRequestModel model)
        {
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
                }
                
            };
        }

        public async Task<BooksResponseModel> GetBooks()
        {
            var books = await _bookRepository.Query().Select(b => new BookDTO
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

            return new BooksResponseModel
            {
                Data = books,
                Status = true,
                Message = "Books retrieved successfully"

            };
        }

        public async Task<BooksResponseModel> GetBooksByPublicationDate(DateTime publicationDate)
        {
            var books = await _bookRepository.GetBooksByPublicationDate(publicationDate);

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
