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
            var categoriaDtos = categorias.Select(category => new CategoriaDTO
            {
                Nome = category.Nome,
                Produtos = category.Produtos?.Select(category => new ProdutoDTO
                {
                    Nome = category.Nome,
                    Preco = category.Preco,
                    Descricao = category.Descricao,
                    Estoque = category.Estoque,
                    Ano = category.Ano,
                    ImagemUrl = category.ImagemUrl,
                    CategoriaId = category.CategoriaId,
                    MarcaId = category.MarcaId
                }).ToList()
            }).ToList();
            return Ok(categoriaDtos);
        }

        [HttpGet("{id}", Name = "GetCategoriaPorId")]
        public ActionResult<Categoria> GetCategoriaPorId(Guid id)
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

        [HttpPut("{id:guid}", Name = "UpdateCategoria")]
        public IActionResult UpdateCategoria(Guid id, [FromBody] Categoria categoria)
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
        public IActionResult DeleteCategoria(Guid id)
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