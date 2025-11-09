using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll(int pageNumber, int pageQuantity);
        public Category GetById(Guid id);
        public Category Add(Category category);
        void Update(Category category);
        void Remove(Guid id);
    }
}
