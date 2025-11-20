using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetBrand")]
        public IActionResult GetBrands(int pageNumber = 1, int pageQuantity = 10)
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
                    return NotFound(new { message = "Brand não encontrada." });
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "admin")]
        [HttpPut("{id:guid}", Name = "UpdateBrand")]
        public IActionResult UpdateBrand(Guid id, [FromBody] Brand brand)
        {
            if (id != brand.Id)
            {
                return BadRequest(new { message = "ID da brand não corresponde." });
            }
            try
            {
                _brandService.UpdateBrand(brand);
                return Ok(new { message = "Brand atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id:guid}", Name = "RemoveBrand")]
        public IActionResult DeleteBrand(Guid id)
        {
            try
            {
                _brandService.DeleteBrand(id);
                return Ok(new { message = "Brand deletada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
