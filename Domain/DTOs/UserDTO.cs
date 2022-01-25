
using Domain.Enums;
using System.Collections.Generic;

namespace Domain.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }

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

        // public string HashSalt { get; set; }

        public UserType UserType { get; set; }
        public IEnumerable<RoleDTO> Roles { get; set; } = new List<RoleDTO>();

    }

    
}
