using Domain.DTOs;
using Domain.Interfaces.Services;
using Domain.Models;
using Domain.Repositories;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<BaseResponse> AddAuthor(CreateAuthorRequestModel model)
        {
            var author = new Author
            {
                
                FirstName = model.FirstName,
                LastName = model.LastName,
                Biography = model.Biography
            };
              await _authorRepository.AddAsync(author);
            await _authorRepository.SaveChangesAsync();
            return new BaseResponse
            {
                Status = true,
                Message = "Successfully Added"
            };
        }

        public void DeleteAuthor(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorResponseModel> GetAuthor(int id)
        {
            var author = await _authorRepository.GetAsync(id);
            /*if (author == null)
            {
                throw new NotFoundException("Role does not exist");
            }*/
            return new AuthorResponseModel
            {
                Data = new AuthorDTO
                {
                    Id = id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    Biography = author.Biography
                },
                Status = true,
                Message = "Successful"
            };

        }

        public Task<AuthorsResponseModel> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateAuthor(int id, UpdateAuthorRequestModel model)
        {
            throw new NotImplementedException();
        }
    }
}
