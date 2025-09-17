using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;
        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }
        public void DeleteCategoria(Guid id)
        {
            var categoria = ObterCategoriaPorId(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }

        public List<Categoria> GetCategorias()
        {
            return _context.Categorias
                .Include(c => c.Produtos)
                .ToList();
        }

        public Categoria ObterCategoriaPorId(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID inválido.");
            }
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            return categoria;
        }

        public Categoria PostCategoria(Categoria categoria)
        {
            var novaCategoria = new Categoria
            {
                Nome = categoria.Nome

            };
            _context.Categorias.Add(novaCategoria);
            _context.SaveChanges();
            return novaCategoria;
        }

        public void UpdateCategoria(Categoria categoria)
        {
            var categoriaExistente = ObterCategoriaPorId(categoria.Id);
            if (categoriaExistente != null)
            {
                categoriaExistente.Nome = categoria.Nome;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }
    }
}