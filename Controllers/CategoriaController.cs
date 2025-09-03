using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;
        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet(Name = "GetCategorias")]
        public IActionResult GetCategorias()
        {
            var categorias = _categoriaService.GetCategorias();
            var categoriaDtos = categorias.Select(c => new CategoriaDTO
            {
                Nome = c.Nome,
                Produtos = c.Produtos?.Select(p => new ProdutoDTO
                {
                    Nome = p.Nome,
                    Preco = p.Preco,
                    Descricao = p.Descricao,
                    Estoque = p.Estoque,
                    Ano = p.Ano,
                    ImagemUrl = p.ImagemUrl,
                    CategoriaId = p.CategoriaId,
                    MarcaId = p.MarcaId
                }).ToList()
            }).ToList();
            return Ok(categoriaDtos);
        }

        [HttpGet("{id}", Name = "GetCategoriaPorId")]
        public ActionResult<Categoria> GetCategoriaPorId(int id)
        {
            var categoria = _categoriaService.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return Ok(categoria);
        }

        [HttpPost(Name = "PostCategoria")]
        public ActionResult<Categoria> PostCategoria([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                var categoria = _categoriaService.PostCategoria(categoriaDto);
                return CreatedAtRoute("GetCategoriaPorId", new { id = categoria.Id }, categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdateCategoria")]
        public IActionResult UpdateCategoria(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest(new { message = "ID da categoria não corresponde." });
            }

            try
            {
                _categoriaService.UpdateCategoria(categoria);
                return Ok(new { message = "Categoria atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteCategoria")]
        public IActionResult DeleteCategoria(int id)
        {
            try
            {
                _categoriaService.DeleteCategoria(id);
                return Ok(new { message = "Categoria deletada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}