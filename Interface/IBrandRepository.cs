using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface IBrandRepository
    {
        public List<Brand> GetAll(int pageNumber, int pageQuantity);
        public Brand GetById(Guid id);
        public Brand AddFromDTO(BrandDTO brand);
        public Brand Add(Brand brand);
        void Update(Brand brand);
        void Delete(Guid id);
    }
}
