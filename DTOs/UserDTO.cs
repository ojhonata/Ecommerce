using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTOs
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cep { get; set; }
        public int Number { get; set; }
        public string Road { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cep { get; set; }
        public int Number { get; set; }
        public string Role { get; set; } = "User";
    }

    public class UpdateUserDTO
    {
        public string Name { get; set; }
        public string Cep { get; set; }
        public int Number { get; set; }
        public string Road { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
