namespace Ecommerce.DTOs
{
    public class PageResponseDTO<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
