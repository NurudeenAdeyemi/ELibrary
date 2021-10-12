using Domain.Models;
using Domain.Repositories;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }
        public Task<Author> GetByAuthorNameAsync(string firstName)
        {
            throw new NotImplementedException();
        }
    }
}
