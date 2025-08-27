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
            throw new NotImplementedException();
        }

        public List<string> GetUsuarios()
        {
            List<Usuario> usuarios = _usuarioRepository.GetUsuarios();
            List<string> nomes = new List<string>();
            foreach (var usuario in usuarios)
            {
                nomes.Add(usuario.Nome);
            }
            return nomes;
        }
    }
}