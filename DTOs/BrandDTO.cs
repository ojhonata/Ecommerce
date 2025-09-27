using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class BrandDTO
    {
        public string Nome { get; set; }
        public string ImagemURL { get; set; }

        public List<CarDTO> Produtos { get; set; }
    }
}