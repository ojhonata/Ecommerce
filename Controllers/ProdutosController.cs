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
            try
            {
                var produtoDtos = _produtoService.GetProdutos();
                return Ok(produtoDtos);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id:guid}", Name = "GetProdutoPorId")]
        public ActionResult<Produto> GetProdutoPorId(Guid id)
        {
            try
            {
                var produto = _produtoService.ObterProdutoPorId(id);
                if (produto == null)
                {
                    return NotFound(new { message = "Produto não encontrado." });
                }
                return Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // [HttpPost(Name = "PostProduto")]
        // public ActionResult<Produto> PostProduto([FromForm] ProdutoDTO produtoDto)
        // {
        //     try
        //     {
        //         var filePath = Path.Combine("Storage", produtoDto.ImagemUrl.FileName);

        //         using Stream fileStream = new FileStream(filePath, FileMode.Create);
        //         produtoDto.ImagemUrl.CopyTo(fileStream);

        //         var produto = _produtoService.PostProduto(produtoDto);
        //         return CreatedAtRoute("GetProdutoPorId", new { id = produto.Id }, produto);
        //     }
        //     catch (Exception ex)
        //     {
        //         return BadRequest(new { message = ex.Message });
        //     }
        // }

        [HttpPut("{id:guid}", Name = "UpdateProduto")]
        public IActionResult UpdateProduto(Guid id, [FromBody] Produto produto)
        {
            if (id != produto.Id)
            {
                return BadRequest(new { message = "ID do produto inválido." });
            }
            try
            {
                _produtoService.UpdateProduto(produto);
                return Ok(new { message = "Produto atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:guid}", Name = "DeleteProduto")]
        public IActionResult DeleteProduto(Guid id)
        {
            try
            {
                _produtoService.DeleteProduto(id);
                return Ok(new { message = "Produto deletado com sucesso." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}