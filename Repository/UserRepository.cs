using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<User> GetUsers()
        {
            return _context.Users.Select(user => user).ToList();
        }

        public User PostUser(UserDTO user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }
    }
}
