using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("cars")]
    public class Car
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The car's name is required.")]
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public int Stock { get; set; }
        [DataType(DataType.Date)]
        public int Year { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Engine { get; set; }
        public int Speed { get; set; }
        public string ImageUrl { get; set; }
        public string InnerImageUrl { get; set; }
        public string ImageEngineUrl { get; set; }
        public string VideoDemoUrl { get; set; }
        public string Model3DUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }
    }
}
