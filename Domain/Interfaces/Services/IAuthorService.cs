using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface IAuthorService
    {
        public Task<AuthorResponseModel> GetAuthor(int id);

        public Task<AuthorsResponseModel> GetAuthors();

        public Task<BaseResponse> UpdateAuthor(int id, UpdateAuthorRequestModel model);

        public Task<BaseResponse> AddAuthor(CreateAuthorRequestModel model);

        public void DeleteAuthor(int id);
    }
}
