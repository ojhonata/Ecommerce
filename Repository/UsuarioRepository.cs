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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Usuario> GetUsuarios()
        {
            return _context.Usuarios.ToList(); // retorna a lista de usuários do banco de dados
        }

        public Usuario PostUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Senha = usuarioDTO.Senha
            };

            _context.Usuarios.Add(usuario); // adiciona o novo usuário ao contexto
            _context.SaveChanges(); // salva as mudanças no banco de dados
            return usuario;
        }

    }
        
    
}