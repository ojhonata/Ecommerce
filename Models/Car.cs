using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("cars")]
    public class Car
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int Ano { get; set; }
        public string ImagemUrl { get; set; }
        public Guid CategoriaId { get; set; }
        public Category Categoria { get; set; }
        public Guid MarcaId { get; set; }
        public Brand Marca { get; set; }

    }
}