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
    public class UserRepository: BaseRepository<User>, IUserRepository
    {
        public UserRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetSelectedRoles(IList<int> ids)
        {
            return await _context.Roles
                .Where(c => ids.Contains(c.Id)).ToListAsync();
        }
    }
}
