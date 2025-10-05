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

        // Perfeito para retornar dados completos para o front-end
        public BrandDTO Marca { get; set; } 
    }

    // ESTE DTO PRECISA DE CORREÇÃO
    public class CreateCarDTO
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        public int Ano { get; set; }
        public IFormFile Imagem { get; set; } // Para o upload
        public Guid CategoriaId { get; set; }
        public Guid MarcaId { get; set; }     // Apenas o ID é necessário para criar

        // public BrandDTO Marca { get; set; } // <-- REMOVA ESTA LINHA
    }
}