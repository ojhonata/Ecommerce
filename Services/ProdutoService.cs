using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public void DeleteProduto(int id)
        {
            throw new NotImplementedException();
        }

        public List<Produto> GetProdutos()
        {
            return _produtoRepository.GetProdutos();
        }

        public Produto ObterProdutoPorId(int id)
        {
            throw new NotImplementedException();
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

        public void UpdateProduto(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}