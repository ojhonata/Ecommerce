using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("brands")]
    public class Brand
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The brand name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
        public string BgBrand { get; set; }
        public string LogoBrand { get; set; }

        public virtual ICollection<Car>? Cars { get; set; }
    }
}
