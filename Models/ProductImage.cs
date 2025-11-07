namespace Ecommerce.Models
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public Guid ProductId { get; set; }
        public Car Product { get; set; }
    }
}
