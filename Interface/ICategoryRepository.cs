using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface ICategoryRepository
    {
        public List<Category> GetCategories();
        public Category GetCategoryById(Guid id);
        public Category PostCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Guid id);
    }
}