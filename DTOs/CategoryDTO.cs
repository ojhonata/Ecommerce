using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<CarDTO>? Produtos { get; set; }
    }
}
