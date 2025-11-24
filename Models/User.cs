using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    [Table("users")]
    public class User
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "A password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Cep { get; set; }
        public int Number { get; set; }
        public string Road { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Role { get; set; } = "User";

    }
}
