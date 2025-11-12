using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class BrandDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string ImagemURL { get; set; }
    }

    public class CreateBrandDto
    {
        public string Nome { get; set; }
        public IFormFile Imagem { get; set; }
    }
}
