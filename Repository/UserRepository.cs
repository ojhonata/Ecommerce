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

        public User PostUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User GetByEmail(string email)
        {
            return _context.Users.FirstOrDefault(user => user.Email == email);
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
