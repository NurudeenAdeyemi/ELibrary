using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> GetByAuthorNameAsync(string firstName);
    }
}
