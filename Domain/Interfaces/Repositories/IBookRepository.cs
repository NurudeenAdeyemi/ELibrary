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

        Task<IList<BookDTO>> GetBooksByPublicationDate(DateTime publicationDate);

        Task<IList<BookDTO>> GetBooksByPublisher(string publisher);

        Task<IList<BookDTO>> GetBooksByCategory(int categoryId);

        Task<IList<BookDTO>> GetBooksByAuthor(int authorId);

        Task<IList<BookDTO>> GetBooksByAvailabilityStatus(BookAvailabilityStatus availabilityStatus);

        Task<IList<BookDTO>> GetBooksByAccessibilityStatus(BookAccessibilityStatus accessibilityStatus);
    }
}
