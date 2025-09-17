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
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet(Name = "GetUsuarios")]
        public IActionResult GetUsuarios()
        {
            try
            {
                var usuarios = _usuarioService.GetUsuarios();
                var usuarioDtos = usuarios.Select(u => new UsuarioDTO
                {
                    Nome = u.Nome,
                    Email = u.Email,
                    Senha = u.Senha
                }).ToList();
                return Ok(usuarioDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost(Name = "PostUsuario")]
        public IActionResult PostUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            try
            {
                Usuario usuario = _usuarioService.PostUsuario(usuarioDTO);
                return Ok(usuario);
            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}