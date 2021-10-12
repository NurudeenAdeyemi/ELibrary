using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class CreateAuthorRequestModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Biography { get; set; }
    }

    public class UpdateAuthorRequestModel
    {
        public string Biography { get; set; }
    }
    public class AuthorsResponseModel : BaseResponse
    {
        public IEnumerable<AuthorDTO> Data { get; set; } = new List<AuthorDTO>();
    }

    public class AuthorResponseModel : BaseResponse
    {
        public AuthorDTO Data { get; set; }
    }
}
