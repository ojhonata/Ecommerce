using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        public Usuario PostUsuario(UsuarioDTO usuarioDTO)
        {
            if(string.IsNullOrEmpty(usuarioDTO.Nome) || string.IsNullOrEmpty(usuarioDTO.Email) || string.IsNullOrEmpty(usuarioDTO.Senha))
            {
                throw new ArgumentException("Nome, Email e Senha são obrigatórios.");
            }
            return _usuarioRepository.PostUsuario(usuarioDTO);
        }

        public List<Usuario> GetUsuarios()
        {
            return _usuarioRepository.GetUsuarios();
        }
    }
}