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
        public List<BrandDTO> GetBrands(int pageNumber, int pageQuantity);
        public Brand GetBrandById(Guid id);
        public Brand PostBrandCloudinary(CreateBrandDto dto);
        public Brand UpdateBrand(Guid id, UpdateBrandDto dto);
        void DeleteBrand(Guid id);
    }
}
