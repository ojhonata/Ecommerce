using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("produtos")]
    public class Produto
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int Ano { get; set; }
        public string ImagemUrl { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int MarcaId { get; set; }
        public Marca Marca { get; set; }

    }
}