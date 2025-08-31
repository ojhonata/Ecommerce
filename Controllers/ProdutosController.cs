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
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoService _produtoService;

        public ProdutosController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet(Name = "GetProdutos")]
        public IActionResult GetProdutos()
        {
            List<string> produtos = _produtoService.GetProdutos();
            return Ok(produtos);
        }

        [HttpGet("{id}", Name = "GetProdutoPorId")]
        public ActionResult<Produto> GetProdutoPorId(int id)
        {
            var produto = _produtoService.ObterProdutoPorId(id);
            if (produto == null)
            {
                return NotFound();
            }
            return Ok(produto);
        }

        [HttpPost(Name = "PostProduto")]
        public ActionResult<Produto> PostProduto([FromBody] ProdutoDTO produtoDto)
        {
            try
            {
                var produto = _produtoService.PostProduto(produtoDto);
                return CreatedAtRoute("GetProdutoPorId", new { id = produto.Id }, produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}", Name = "UpdateProduto")]
        public IActionResult UpdateProduto(int id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest(new { message = "ID do produto inválido." });
            }
            try
            {
                _produtoService.UpdateProduto(produto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}", Name = "DeleteProduto")]
        public IActionResult DeleteProduto(int id)
        {
            try
            {
                _produtoService.DeleteProduto(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}