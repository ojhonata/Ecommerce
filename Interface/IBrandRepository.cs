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
        public List<Brand> GetBrands();
        public Brand GetBrandById(Guid id);
        public Brand PostBrand(BrandDTO brand);
        void UpdateBrand(Brand brand);
        void DeleteBrand(Guid id);
    }
}