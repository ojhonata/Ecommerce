using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface IProdutoService
    {
        public Produto ObterProdutoPorId(int id);
        public List<Produto> GetProdutos();
        public Produto PostProduto(ProdutoDTO produto);
        void UpdateProduto(Produto produto);
        void DeleteProduto(int id);
    }
}