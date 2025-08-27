using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context; // variavel que vai acessar o banco

        public UsuariosController(AppDbContext appDbContext)
        {
            _context = appDbContext; // inicializa o contexto do banco de dados
        }

        [HttpGet(Name = "GetUsuarios")]
        public IActionResult GetUsuarios()
        {
            var usuarios = _context.Usuarios.ToList(); // busca a lista de usuários do banco de dados
            return Ok(usuarios); // retorna a lista de usuários como resposta HTTP 200
        }

        [HttpPost(Name = "PostUsuario")]
        public IActionResult PostUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            var usuario = new Usuario
            {
                Nome = usuarioDTO.Nome,
                Email = usuarioDTO.Email,
                Senha = usuarioDTO.Senha
            };

            _context.Usuarios.Add(usuario); // adiciona o novo usuário ao contexto
            _context.SaveChanges(); // salva as mudanças no banco de dados
            return Ok(usuario);
        }
    }
}