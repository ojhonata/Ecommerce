using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface IUserService
    {
        public List<UserDTO> GetUsers();
        public User GetByEmail(string email);
        public User PostUser(UserDTO user);
    }
}
