using Domain.DTOs;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetBookByTitle(string title);

        Task<IList<BookDTO>> GetBooksByPublicationDate(int publicationDate);

        Task<IList<BookDTO>> GetBooksByPublisher(string publisher);

        Task<IList<BookDTO>> GetBooksByCategory(int categoryId);

        Task<IList<BookDTO>> GetBooksByAuthor(int authorId);

        Task<IList<BookDTO>> GetBooksByAvailabilityStatus(BookAvailabilityStatus availabilityStatus);

        Task<IList<BookDTO>> GetBooksByAccessibilityStatus(BookAccessibilityStatus accessibilityStatus);

        Task<BookLending> CheckoutBookItem(BookLending bookLending);

        BookLending ReturnBookItem(BookLending bookLending);

        int NumberOfBooksBorrowed(int userId, bool bookReturned);

        Task<IList<BookDTO>> GetListOfBooks(int userId, bool bookReturned);

        Task<BookLending> GetBookBorrowed(int bookId, int userId, bool bookReturned);
    }
}
