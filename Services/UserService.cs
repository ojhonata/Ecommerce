using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usuarioRepository;
        public UserService(IUserRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public User PostUser(UserDTO userDTO)
        {
            if(string.IsNullOrEmpty(userDTO.Nome) || string.IsNullOrEmpty(userDTO.Email) || string.IsNullOrEmpty(userDTO.Senha))
            {
                throw new ArgumentException("Nome, Email e Senha são obrigatórios.");
            }
            return _usuarioRepository.PostUser(userDTO);
        }

        public List<UserDTO> GetUsers()
        {
            var users = _usuarioRepository.GetUsers();
            var userDtos = users.Select(u => new UserDTO
            {
                Nome = u.Nome,
                Email = u.Email,
                Senha = u.Senha
            }).ToList();
            return userDtos;
        }
    }
}