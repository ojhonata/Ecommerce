using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
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

        // CORREÇÃO: Removendo "GetCategory" para usar a rota base /api/Category
        [HttpGet] 
        public IActionResult GetCategory(int pageNumber = 1, int pageQuantity = 10)
        {
            try
            {
                var categories = _categoryService.GetCategories(pageNumber, pageQuantity);
                return Ok(categories);
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
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddCategory")]
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}", Name = "UpdateCategoiry")]
        public IActionResult UpdateCategory(Guid id, [FromBody] Category category)
        {
            if (id != category.Id)
            {
                return BadRequest(new { message = "The category ID does not match." });
            }
            try
            {
                _categoryService.UpdateCategory(category);
                return Ok(new { message = "Category updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}", Name = "RemoveCategroy")]
        public IActionResult DeleteCategory(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid category ID." });
            }
            try
            {
                _categoryService.DeleteCategory(id);
                return Ok(new { message = "Category successfully deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}