using Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
   
        public class CreateCategoryRequestModel
        {
            public string Name { get; set; }
        }

        public class UpdateCategoryRequestModel
        {
            public string Name { get; set; }
        }

        public class CategoryResponseModel : BaseResponse
        {
            public CategoryDTO Data { get; set; }
        }

        public class CategoriesResponseModel :BaseResponse
        {
            public IEnumerable<CategoryDTO> Data { get; set; } = new List<CategoryDTO>();
        }
    
}
