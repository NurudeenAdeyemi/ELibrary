﻿using Domain.DTOs;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository; 

        public UserService(IUserRepository userRePository)
        {
            _userRepository = userRePository;
        }
        public async Task<BaseResponse> AddUser(CreateUserRequestModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Country = model.Country,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Status = model.Status,
                University = model.University,
                UserType = model.UserType,
                PasswordHash = model.Password,
                LibrarianIdentificationNumber = model.LibrarianIdentificationNumber,
                LibraryIdentificationNumber = model.LibraryIdentificationNumber,

            };

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

            return new BaseResponse
            {
                Status = true,
                Message = "User added successfully"
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
                    LibrarianIdentificationNumber = user.LibrarianIdentificationNumber,
                    LibraryIdentificationNumber = user.LibraryIdentificationNumber,
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
