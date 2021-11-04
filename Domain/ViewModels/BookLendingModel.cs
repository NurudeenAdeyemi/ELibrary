using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{

    public class CheckOutBookRequestModel
    {
        public int UserId { get; set; }

        public int BookId { get; set; }
    }

    public class BooklendingResponseModel : BaseResponse
    {
        public BookLendingDTO Data { get; set; }
    }
}
