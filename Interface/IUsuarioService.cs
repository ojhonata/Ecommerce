using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface IUsuarioService
    {
        public List<string> GetUsuarios();
        public Usuario PostUsuario(UsuarioDTO usuarioDTO);
    }
}