using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _usuarioRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }

        public User PostUser(User user)
        {
            if (
                string.IsNullOrEmpty(user.Nome)
                || string.IsNullOrEmpty(user.Email)
                || string.IsNullOrEmpty(user.Senha)
            )
            {
                throw new ArgumentException("Nome, Email e Senha são obrigatórios.");
            }
            return _usuarioRepository.PostUser(user);
        }

        public List<UserDTO> GetUsers()
        {
            var users = _usuarioRepository.GetUsers();
            return _mapper.Map<List<UserDTO>>(users);
        }
    }
}
