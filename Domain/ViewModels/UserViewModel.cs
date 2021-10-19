﻿using Domain.DTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class CreateUserRequestModel
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

        public string Password { get; set; }

        public string HashSalt { get; set; }

        public UserType UserType { get; set; }
    }

    public class UpgradeLibraryUserRequestModel
    {
        public UserType UserType { get; set; }
    }

    public class UpdateUserStatusRequestModel
    {
        public AccountStatus Status { get; set; }
    }

    public class UserResponseModel : BaseResponse
    {
        public UserDTO Data { get; set; }
    }

    public class UsersResponseModel : BaseResponse
    {
        public IEnumerable<BookDTO> Data { get; set; } = new List<BookDTO>();
    }
}
