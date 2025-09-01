using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface ICategoriaService
    {
        public List<Categoria> GetCategorias();
        public Categoria ObterCategoriaPorId(int id);
        public Categoria PostCategoria(CategoriaDTO categoria);
        void UpdateCategoria(Categoria categoria);
        void DeleteCategoria(int id);
    }
}