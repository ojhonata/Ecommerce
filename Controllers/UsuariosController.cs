using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
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
            var usuarios = _context.ecommerce.ToList(); // busca a lista de usuários do banco de dados
            return Ok(usuarios); // retorna a lista de usuários como resposta HTTP 200
        }
    }
}