namespace Ecommerce.DTOs
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int Ano { get; set; }
        public string Motor { get; set; }
        public int Velocidade { get; set; }
        public string ImagemUrl { get; set; }
        public string ImagemInteriorUrl { get; set; }
        public string ImagemMotorUrl { get; set; }
        public string VideoDemoUrl { get; set; }
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
        public string Motor { get; set; }
        public int Velocidade { get; set; }
        public IFormFile Imagem { get; set; }
        public IFormFile ImagemInterior { get; set; }
        public IFormFile ImagemMotor { get; set; }
        public IFormFile VideoDemoUrl { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid MarcaId { get; set; }
    }
}
