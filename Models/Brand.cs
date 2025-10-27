using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("brands")]
    public class Brand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O nome da marca é obrigatório.")]
        public string Nome { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImagemURL { get; set; }

        public virtual ICollection<Car> Produtos { get; set; }
    }
}
