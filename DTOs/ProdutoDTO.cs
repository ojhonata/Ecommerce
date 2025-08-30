using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class ProdutoDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int CategoriaId { get; set; }
        public int MarcaId { get; set; }
        public int Ano { get; set; }
        public string ImagemUrl { get; set; }
    }
}