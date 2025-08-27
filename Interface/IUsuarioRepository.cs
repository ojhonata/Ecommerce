using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

// aqui voce vai adicionar seus métodos de acesso ao banco de dados
// exemplo: listar usuários, buscar usuário por id, adicionar usuário, atualizar usuário, deletar usuário, neste caso estou dizendo para a minha classe usuario ele vai poder obter uma lista de usuarios
namespace Ecommerce.Interface
{
    public interface IUsuarioRepository
    {
        public List<Usuario> GetUsuarios();
        public Usuario PostUsuario(UsuarioDTO usuarioDTO);
    }
}