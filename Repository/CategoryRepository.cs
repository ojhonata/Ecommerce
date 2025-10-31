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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Remove(Guid id)
        {
            var category = GetById(id);
            if (category != null)
            {
                _context.Categorias.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Categoria não encontrada.");
            }
        }

        public List<Category> GetAll(int pageNumber, int pageQuantity)
        {
            return _context
                .Categorias.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Include(c => c.Produtos)
                .ToList();
        }

        public Category GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID inválido.");
            }
            var categoria = _context.Categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }
            return categoria;
        }

        public Category Add(Category category)
        {
            var newCategories = new Category { Nome = category.Nome };
            _context.Categorias.Add(newCategories);
            _context.SaveChanges();
            return newCategories;
        }

        public void Update(Category category)
        {
            var existing = _context.Categorias.Find(category.Id);
            if (existing != null)
            {
                throw new ArgumentException("Categoria não encontrada.");
            }
            
            _context.Entry(existing).CurrentValues.SetValues(category);
            _context.SaveChanges();
        }
    }
}
