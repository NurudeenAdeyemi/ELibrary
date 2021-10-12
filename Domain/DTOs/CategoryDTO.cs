
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }

    public class CreateCategoryRequestModel
    {
        public string Name { get; set; }
    }

    public class UpdateCategoryRequestModel
    {
        public string Name { get; set; }
    }
}
