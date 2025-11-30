using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // CORREÇÃO AQUI: Rota principal agora é [HttpGet] para evitar 404
        [HttpGet] 
        public IActionResult GetBrands(int pageNumber = 1, int pageQuantity = 30)
        {
            try
            {
                var brands = _brandService.GetBrands(pageNumber, pageQuantity);
                return Ok(brands);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}", Name = "GetBrandById")]
        public ActionResult<Brand> GetBrandById(Guid id)
        {
            try
            {
                var brand = _brandService.GetBrandById(id);
                if (brand == null)
                {
                    return NotFound(new { message = "Brand not found." });
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("AddCloudinary")]
        public ActionResult<Brand> PostBrandCloudinary([FromForm] CreateBrandDto brandDto)
        {
            try
            {
                var brand = _brandService.PostBrandCloudinary(brandDto);
                return CreatedAtAction(nameof(GetBrandById), new { id = brand.Id }, brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:guid}", Name = "UpdateBrand")]
        public IActionResult UpdateBrand(Guid id, [FromForm] UpdateBrandDto brand)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _brandService.UpdateBrand(id, brand);
                return Ok(new { message = "Brand updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}", Name = "RemoveBrand")]
        public IActionResult DeleteBrand(Guid id)
        {
            try
            {
                _brandService.DeleteBrand(id);
                return Ok(new { message = "Brand successfully deleted." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}