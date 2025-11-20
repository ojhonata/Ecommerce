using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class BrandDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string BgBrand { get; set; }
        public string LogoBrand { get; set; }
        public string Description { get; set; }
    }

    public class CreateBrandDto
    {
        public string Name { get; set; }
        public IFormFile Image { get; set; }
        public IFormFile BgBrand { get; set; }
        public IFormFile LogoBrand { get; set; }
        public string Description { get; set; }
    }
}
