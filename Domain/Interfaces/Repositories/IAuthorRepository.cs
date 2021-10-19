using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetByAuthorNameAsync(string firstName);

        Task<IEnumerable<Author>> GetSelectedAuthors(IList<int> ids);
    }
}
