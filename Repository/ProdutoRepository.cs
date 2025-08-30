using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Repository
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }
        public void DeleteProduto(int id)
        {
            var produto = ObterProdutoPorId(id);
            if (produto != null)
            {
                _context.Produtos.Remove(produto);
                _context.SaveChanges(); // salva as mudanças no banco de dados
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }

        public List<Produto> GetProdutos()
        {
            return _context.Produtos.ToList(); // retorna a lista de produtos do banco de dados
        }

        public Produto ObterProdutoPorId(int id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id); // retorna o produto com o ID especificado
        }

        public Produto PostProduto(ProdutoDTO produto)
        {
            var novoProduto = new Produto
            {
                Nome = produto.Nome,
                Preco = produto.Preco,
                Descricao = produto.Descricao,
                Estoque = produto.Estoque,
                CategoriaId = produto.CategoriaId,
                MarcaId = produto.MarcaId,
                Ano = produto.Ano,
                ImagemUrl = produto.ImagemUrl
            };

            _context.Produtos.Add(novoProduto); // adiciona o novo produto ao contexto
            _context.SaveChanges(); // salva as mudanças no banco de dados
            return novoProduto;
        }

        public void UpdateProduto(Produto produto)
        {
            var produtoExistente = _context.Produtos.Find(produto.Id);
            if (produtoExistente != null)
            {
                produtoExistente.Nome = produto.Nome;
                produtoExistente.Preco = produto.Preco;
                produtoExistente.Descricao = produto.Descricao;
                produtoExistente.Estoque = produto.Estoque;
                produtoExistente.CategoriaId = produto.CategoriaId;
                produtoExistente.MarcaId = produto.MarcaId;
                produtoExistente.Ano = produto.Ano;
                produtoExistente.ImagemUrl = produto.ImagemUrl;

                _context.SaveChanges(); // salva as mudanças no banco de dados
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }
    }
}