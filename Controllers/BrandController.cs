using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
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
        public IActionResult GetBrands()
        {
            try
            {
                var brands = _brandService.GetBrands();
                var brandDtos = brands.Select(m => new BrandDTO
                {
                    Nome = m.Nome,
                    ImagemURL = m.ImagemURL,
                    Produtos = m.Produtos?.Select(p => new CarDTO
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
                return Ok(brandDtos);
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
                    return NotFound(new { message = "Marca não encontrada." });
                }
                return Ok(brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("PostBrnad")]
        public ActionResult<Brand> PostMarca([FromBody] BrandDTO brandDto)
        {
            try
            {
                var brand = _brandService.PostBrand(brandDto);
                return CreatedAtAction(nameof(GetBrandById), new { id = brand.Id }, brand);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
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
                return Ok(new { message = "Marca atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpDelete("{id:guid}", Name = "DeleteBrand")]
        public IActionResult DeleteBrand(Guid id)
        {
            try
            {
                _brandService.DeleteBrand(id);
                return Ok(new { message = "Marca deletada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}