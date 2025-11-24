using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User PostUser(User user);
        public User GetByEmail(string email);
        public void UpdateUser(User user);
    }
}
