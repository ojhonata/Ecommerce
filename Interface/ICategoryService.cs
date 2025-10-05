using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface ICategoryService
    {
        public List<CategoryDTO> GetCategories();
        public CategoryDTO GetCategoryById(Guid id);
        public Category PostCategory(CategoryDTO category);
        void UpdateCategory(Category category);
        void DeleteCategory(Guid id);
    }
}