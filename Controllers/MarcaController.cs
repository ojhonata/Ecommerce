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
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet(Name = "GetMarcas")]
        public IActionResult GetMarcas()
        {
            var marcas = _marcaService.GetMarcas();
            var marcaDtos = marcas.Select(m => new MarcaDTO
            {
                Nome = m.Nome,
                ImagemURL = m.ImagemURL,
                Produtos = m.Produtos?.Select(p => new ProdutoDTO
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
            return Ok(marcaDtos);
        }

        [HttpGet("{id}", Name = "GetMarcaPorId")]
        public ActionResult<Marca> GetMarcaPorId(int id)
        {
            var marca = _marcaService.ObterMarcaPorId(id);
            if (marca == null)
            {
                return NotFound(new { message = "Marca não encontrada." });
            }
            return Ok(marca);
        }

        [HttpPost(Name = "PostMarca")]
        public ActionResult<Marca> PostMarca([FromBody] MarcaDTO marcaDto)
        {
            try
            {
                var marca = _marcaService.PostMarca(marcaDto);
                return CreatedAtRoute("GetMarcaPorId", new { id = marca.Id }, marca);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPut("{id}", Name = "UpdateMarca")]
        public IActionResult UpdateMarca(int id, [FromBody] Marca marca)
        {
            if (id != marca.Id)
            {
                return BadRequest(new { message = "ID da marca não corresponde." });
            }
            try
            {
                _marcaService.UpdateMarca(marca);
                return Ok(new { message = "Marca atualizada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpDelete("{id}", Name = "DeleteMarca")]
        public IActionResult DeleteMarca(int id)
        {
            try
            {
                _marcaService.DeleteMarca(id);
                return Ok(new { message = "Marca deletada com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}