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
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(AppDbContext appDbContext, IUsuarioService usuarioService)
        {
            _context = appDbContext; // inicializa o contexto do banco de dados
            _usuarioService = usuarioService;
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
            Usuario usuario = _usuarioService.PostUsuario(usuarioDTO);
            return Ok(usuario);
        }
    }
}