﻿using Domain.Models;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetByEmail(string email);
        public Task<IEnumerable<Role>> GetSelectedRoles(IList<int> ids);
    }
}
