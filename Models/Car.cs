using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("cars")]
    public class Car
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "O nome do carro é obrigatório.")]
        public string Nome { get; set; }
        [DataType(DataType.Currency)]
        public decimal Preco { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }
        public int Estoque { get; set; }
        [DataType(DataType.Date)]
        public int Ano { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Motor { get; set; }
        public int Velocidade { get; set; }
        public string ImagemUrl { get; set; }
        public string ImagemInteriorUrl { get; set; }
        public string ImagemMotorUrl { get; set; }
        public string VideoDemoUrl { get; set; }
        public Guid CategoriaId { get; set; }
        public Category Categoria { get; set; }
        public Guid MarcaId { get; set; }
        public Brand Marca { get; set; }
    }
}
