using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("categories")]
    public class Category
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O nome da categoria é obrigatório.")]
        public string Nome { get; set; }

        public virtual ICollection<Car>? Produtos { get; set; }
    }
}
