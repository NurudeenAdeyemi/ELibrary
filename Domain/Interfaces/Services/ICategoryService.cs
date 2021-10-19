using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        public Task<CategoryResponseModel> GetCategory(int id);

        public Task<CategoriesResponseModel> GetCategories();

        public Task<CategoriesResponseModel> GetSelectedCategories(IList<int> ids);

        public Task<BaseResponse> Addcategory(CreateCategoryRequestModel model);

        public Task<BaseResponse> UpdateCategory(int id, UpdateCategoryRequestModel model);

        public void DeleteCategory(int id);

    }
}
