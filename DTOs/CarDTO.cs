using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class CarDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int Ano { get; set; }
        public string ImagemUrl { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid MarcaId { get; set; }

    }
}