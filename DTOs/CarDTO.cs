// DTOs/CarDTO.cs

namespace Ecommerce.DTOs
{
    // ESTE DTO ESTÁ CORRETO
    public class CarDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int Ano { get; set; }
        public string ImagemUrl { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid MarcaId { get; set; }

        public BrandDTO Marca { get; set; }
    }

    public class CreateCarDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int Ano { get; set; }
        public IFormFile Imagem { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid MarcaId { get; set; }
    }
}
