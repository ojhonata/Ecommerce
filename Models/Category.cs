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
        [Required(ErrorMessage = "The category name is required.")]
        public string Name { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
    }
}
