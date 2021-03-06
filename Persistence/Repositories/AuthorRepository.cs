using Domain.Interfaces.Repositories;
using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<Author>> GetSelectedAuthors(IList<int> ids)
        {
            return await _context.Authors
                .Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
