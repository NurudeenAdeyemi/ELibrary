using Domain.DTOs;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Identity;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;
using ELibrary.Infrastructure.Persistence.Integrations.Email;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly IMailSender _mailSender;
        private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRePository, IIdentityService identityService, IMailSender mailSender, UserManager<User> userManager)
        {
            _userRepository = userRePository;
            _identityService = identityService;
            _mailSender = mailSender;
            _userManager = userManager;
        }
        public async Task<BaseResponse> AddUser(CreateUserRequestModel model)
        {
            var emailExists = await _userRepository.ExistsAsync(u => u.Email == model.Email);

            if (emailExists)
            {
                throw new BadRequestException($"{model.Email} already used by a user");
            }
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Status = AccountStatus.ACTIVE,
                University = model.University,
                UserType = model.UserType,
                VerificationStatus = VerificationStatus.NotVerified,
                HashSalt = Guid.NewGuid().ToString()
            };
            if (user.UserType == UserType.Admin)
            {
                user.LibraryNumber = $"ELIB_ADMIN{Guid.NewGuid().ToString().Substring(0, 3)}";
            }
            else if (user.UserType == UserType.Librarian)
            {
                user.LibraryNumber = $"ELIB_LIBRARIAN{Guid.NewGuid().ToString().Substring(0, 3)}";
            }
            else if (user.UserType == UserType.LibraryUser)
            {
                user.LibraryNumber = $"ELIB_USER{Guid.NewGuid().ToString().Substring(0, 3)}";
            }
            else if(string.IsNullOrEmpty(model.UserType.ToString()))
            {
                throw new BadRequestException($"User type is required");
            };
            user.PasswordHash = _identityService.GetPasswordHash(model.Password, user.HashSalt);

            var roles = await _userRepository.GetSelectedRoles(model.Roles);
            foreach(var role in roles)
            {
                var userRole = new UserRole
                {
                    User = user,
                    UserId = user.Id,
                    Role = role,
                    RoleId = role.Id

                };

                user.UserRoles.Add(userRole);
            }

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            var baseResetLink = "https://localhost:44307/auth/verify";
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var activationLink = $"{baseResetLink}?id={user.Id}&token={HttpUtility.UrlEncode(token)}";

            await _mailSender.SendWelcomeMail(user.Email, $"{ user.FirstName} {user.LastName}", activationLink);

            return new BaseResponse
            {
                Status = true,
                Message = "User added successfully. Check your email and click on the link to activate your account"
            };
        }

        public async Task<BaseResponse> VerifyEmail(VerifyEmailViewModel model)
        {
            //var user = await _userManager.FindByIdAsync(model.Id.ToString());
            var user = await _userRepository.GetAsync(model.Id);
            if (user == null)
            {
                throw new NotFoundException($"User not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(model.Token));
            if (result.Succeeded)
            {
                user.VerificationStatus = VerificationStatus.Verified;
               
            }
            await _userRepository.UpdateAsync(user);
            await _mailSender.SendVerifyMail(user.Email, $"{user.FirstName} {user.LastName}");
            return new BaseResponse
            {
                Status = true,
                Message = "Email successfully verified."
            };
        }

        public async Task<BaseResponse> DeleteUser(int id)
        {
            var user = await _userRepository.GetAsync(id);

            await _userRepository.DeleteAsync(user);
            await _userRepository.SaveChangesAsync();

            return new BaseResponse
            {
                Status = true,
                Message = "User deleted successfully"
            };
        }

        public async Task<UserResponseModel> GetUser(int id)
        {
            var user = await _userRepository.Query().Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync(u => u.Id == id);

            return new UserResponseModel
            {
                Data = new UserDTO
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Country = user.Country,
                    University = user.University,
                    LibrarianIdentificationNumber = user.LibraryNumber,
                    LibraryIdentificationNumber = user.LibraryNumber,
                    PhoneNumber = user.PhoneNumber,
                    Status = user.Status,
                    PasswordHash = user.PasswordHash,
                    UserType = user.UserType,
                    Roles = user.UserRoles.Select(r => new RoleDTO
                    {
                        Id = r.Id,
                        Description = r.Role.Description,
                        Name = r.Role.Name

                    }),

                },
                Status = true,
                Message = "User retrieved Successfully"

            };
        }

        public Task<UsersResponseModel> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateUser(int id, UpdateUserStatusRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpgradeUser(int id, UpgradeLibraryUserRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
