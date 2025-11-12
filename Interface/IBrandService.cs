using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface IBrandService
    {
        public Brand PostBrand(CreateBrandDto dto);
        public List<BrandDTO> GetBrands(int pageNumber, int pageQuantity);
        public Brand GetBrandById(Guid id);
        public Brand PostBrand(BrandDTO brand);
        public Brand PostBrandCloudinary(CreateBrandDto dto);
        void UpdateBrand(Brand brand);
        void DeleteBrand(Guid id);
    }
}
