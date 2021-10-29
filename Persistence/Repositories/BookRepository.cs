using Domain.DTOs;
using Domain.Enums;
using Domain.Interfaces.Repositories;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task<Book> GetBookByTitle(string title)
        {
            return await _context.Books
                .Include(ba => ba.BookAuthors)
                .ThenInclude(a => a.Author)
                .Include(bc => bc.BookCategories)
                .ThenInclude(c => c.Category)
                .SingleOrDefaultAsync(c => c.Title == title);
        }

        public async Task<IList<BookDTO>> GetBooksByAccessibilityStatus(BookAccessibilityStatus accessibilityStatus)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .Where(b => b.AccessibilityStatus == accessibilityStatus).Select(b => new BookDTO
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
        }

        public async Task<IList<BookDTO>> GetBooksByAuthor(int authorId)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .Where(b => b.BookAuthors.Any(a => a.AuthorId == authorId)).Select(b => new BookDTO
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

                }).ToListAsync(); ;
        }

        public async Task<IList<BookDTO>> GetBooksByAvailabilityStatus(BookAvailabilityStatus availabilityStatus)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .Where(b => b.AvailabilityStatus == availabilityStatus).Select(b => new BookDTO
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
        }

        public async Task<IList<BookDTO>> GetBooksByCategory(int categoryId)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .Where(b => b.BookCategories.Any(c => c.CategoryId == categoryId)).Select(b => new BookDTO
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
                        Id = a.Id,
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
        }

        public async Task<IList<BookDTO>> GetBooksByPublicationDate(int publicationDate)
        {
            return await _context.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(b => b.Author)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .Where(b => b.PublicationDate.Year == publicationDate).Select(b => new BookDTO
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
        }

        public async Task<IList<BookDTO>> GetBooksByPublisher(string publisher)
        {
            return await _context.Books
                 .Include(b => b.BookAuthors)
                 .ThenInclude(b => b.Author)
                 .Include(b => b.BookCategories)
                 .ThenInclude(b => b.Category)
                 .Where(b => b.Publisher == publisher).Select(b => new BookDTO
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
        }
    }
}
