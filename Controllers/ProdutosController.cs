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
            var produtos = _produtoService.GetProdutos();
            var produtoDtos = produtos.Select(p => new ProdutoDTO
            {
                Nome = p.Nome,
                Preco = p.Preco,
                Descricao = p.Descricao,
                Estoque = p.Estoque,
                Ano = p.Ano,
                ImagemUrl = p.ImagemUrl,
                CategoriaId = p.CategoriaId,
                MarcaId = p.MarcaId
            }).ToList();
            return Ok(produtoDtos);
        }

        [HttpGet("{id}", Name = "GetProdutoPorId")]
        public ActionResult<Produto> GetProdutoPorId(Guid id)
        {
            var produto = _produtoService.ObterProdutoPorId(id);
            if (produto == null)
            {
                return NotFound(new { message = "Produto não encontrado." });
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

        [HttpDelete("{id}", Name = "DeleteProduto")]
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