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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetCategories")]
        public IActionResult GetCategory()
        {
            try
            {
                var categories = _categoryService.GetCategories();
                var categoryDtos = categories.Select(category => new CategoryDTO
                {
                    Nome = category.Nome,
                    Produtos = category.Produtos?.Select(category => new CarDTO
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
                return Ok(categoryDtos);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpGet("{id:guid}", Name = "GetCategoryById")]
        public ActionResult<Category> GetCategoryById(Guid id)
        {
            try
            {
                var category = _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    return NotFound();
                }
                return Ok(category);
            } catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("PostCategory")]
        public ActionResult<Category> PostCategory([FromBody] CategoryDTO categoryDto)
        {
            try
            {
                var category = _categoryService.PostCategory(categoryDto);
                return CreatedAtRoute("GetCategoryById", new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:guid}", Name = "UpdateCategory")]
        public IActionResult UpdateCategory(Guid id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest(new { message = "ID da category não corresponde." });
            }
            try
            {
                _categoryService.UpdateCategory(category);
                return Ok(new { message = "Categoria atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}", Name = "DeleteCategory")]
        public IActionResult DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "ID da category inválido." });
            }
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok(new { message = "Categoria deletada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}