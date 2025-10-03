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
            return _context.Usuarios
            .Select(user => user) // seleciona todos os usuários
            .ToList(); // retorna a lista de usuários do banco de dados
        }

        public User PostUser(UserDTO userDTO)
        {
            var user = new User
            {
                Nome = userDTO.Nome,
                Email = userDTO.Email,
                Senha = userDTO.Senha
            };

            _context.Usuarios.Add(user); // adiciona o novo usuário ao contexto
            _context.SaveChanges(); // salva as mudanças no banco de dados
            return user;
        }

    }
        
    
}