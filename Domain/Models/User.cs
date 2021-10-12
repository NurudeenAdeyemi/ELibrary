
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Country { get; set; }

        public string University { get; set; }

        public string LibraryIdentificationNumber { get; set; }

        public string LibrarianIdentificationNumber { get; set; }

        public AccountStatus Status { get; set; }

        public string PasswordHash { get; set; }

        public string HashSalt { get; set; }


        public UserType UserType { get; set; }

        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

    }
}
