namespace Ecommerce.DTOs
{
    public class CarFilterDTO
    {
        public string? Name { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public int? YearMin { get; set; }
        public int? YearMax { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
