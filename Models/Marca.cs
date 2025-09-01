using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("marcas")]
    public class Marca
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string ImagemURL { get; set; }

        public ICollection<Produto> Produtos { get; set; }
    }
}