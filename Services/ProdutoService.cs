using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public List<ProdutoDTO> GetProdutos()
        {
            var produtos = _produtoRepository.GetProdutos();
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
            
            return produtoDtos;
        }

        public Produto ObterProdutoPorId(Guid id)
        {
            var produto = _produtoRepository.ObterProdutoPorId(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado.");
            }
            return produto;
        }

        public Produto PostProduto(ProdutoDTO produto)
        {
            if (string.IsNullOrEmpty(produto.Nome))
            {
                throw new Exception("O nome do produto é obrigatório.");
            }
            if (produto.Preco <= 0)
            {
                throw new Exception("O preço do produto deve ser maior que zero.");
            }
            if (produto.Estoque < 0)
            {
                throw new Exception("O estoque do produto não pode ser negativo.");
            }
            return _produtoRepository.PostProduto(produto);
        }

        public void DeleteProduto(Guid id)
        {
            var produto = _produtoRepository.ObterProdutoPorId(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado.");
            }
            _produtoRepository.DeleteProduto(id);   

        }

        public void UpdateProduto(Produto produto)
        {
            var produtoExistente = _produtoRepository.ObterProdutoPorId(produto.Id);
            if (produtoExistente == null)
            {
                throw new Exception("Produto não encontrado.");
            }
            if (string.IsNullOrEmpty(produto.Nome))
            {
                throw new Exception("O nome do produto é obrigatório.");
            }
            if (produto.Preco <= 0)
            {
                throw new Exception("O preço do produto deve ser maior que zero.");
            }
            if (produto.Estoque < 0)
            {
                throw new Exception("O estoque do produto não pode ser negativo.");
            }
            _produtoRepository.UpdateProduto(produto);
        }
    }
}