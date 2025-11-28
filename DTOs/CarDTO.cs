namespace Ecommerce.DTOs
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Year { get; set; }
        public string Engine { get; set; }
        public int Speed { get; set; }
        public string? ImageUrl { get; set; }
        public string? InnerImageUrl { get; set; }
        public string? ImageEngineUrl { get; set; }
        public string? VideoDemoUrl { get; set; }
        public string? Model3DUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }

        public BrandDTO Brand { get; set; }
    }

    public class CreateCarDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int Year { get; set; }
        public string Engine { get; set; }
        public int Speed { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? InnerImage { get; set; }
        public IFormFile? ImageEngine { get; set; }
        public IFormFile? VideoDemoUrl { get; set; }
        public IFormFile? Model3DUrl { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }

    public class UpdateCarDTO
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public int? Stock { get; set; }
        public int? Year { get; set; }
        public string? Engine { get; set; }
        public int? Speed { get; set; }
        public IFormFile? Image { get; set; }
        public IFormFile? InnerImage { get; set; }
        public IFormFile? ImageEngine { get; set; }
        public IFormFile? VideoDemoUrl { get; set; }
        public IFormFile? Model3DUrl { get; set; }
    }
}
