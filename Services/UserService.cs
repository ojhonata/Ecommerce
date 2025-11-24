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
        private readonly IViaCepService _viaCepService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository usuarioRepository,
            IViaCepService viaCepService,
            IMapper mapper
        )
        {
            _usuarioRepository = usuarioRepository;
            _viaCepService = viaCepService;
            _mapper = mapper;
        }

        public User PostUser(CreateUserDto userDto)
        {
            if (
                string.IsNullOrEmpty(userDto.Name)
                || string.IsNullOrEmpty(userDto.Email)
                || string.IsNullOrEmpty(userDto.Password)
                || string.IsNullOrEmpty(userDto.Cep)
            )
            {
                throw new ArgumentException("Name, Email, Password, and Zip Code are required.");
            }

            var cepInfo = _viaCepService.GetCep(userDto.Cep);

            var user = _mapper.Map<User>(userDto);

            user.Road = cepInfo.Logradouro;
            user.Neighborhood = cepInfo.Bairro;
            user.City = cepInfo.Localidade;
            user.State = cepInfo.Uf;

            return _usuarioRepository.PostUser(user);
        }

        public List<UserDTO> GetUsers()
        {
            var users = _usuarioRepository.GetUsers();
            return _mapper.Map<List<UserDTO>>(users);
        }

        public User GetByEmail(string email)
        {
            return _usuarioRepository.GetByEmail(email);
        }

        public void UpdateUser(string email, UpdateUserDTO user)
        {
            var existingUser = _usuarioRepository.GetByEmail(email);
            if (existingUser == null)
                throw new ArgumentException("User not found.");

            _mapper.Map(user, existingUser);

            _usuarioRepository.UpdateUser(existingUser);
        }
    }
}
