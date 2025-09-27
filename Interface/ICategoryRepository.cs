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
        public Category PostCategory(Category categoria);
        void UpdateCategory(Category categoria);
        void DeleteCategory(Guid id);
    }
}