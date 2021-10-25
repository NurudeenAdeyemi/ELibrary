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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {

        public CategoryRepository(LibraryContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetSelectedCategories(IList<int> ids)
        {
            return await _context.Categories
                .Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
