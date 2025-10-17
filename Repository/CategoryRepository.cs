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

        public void DeleteCategory(Guid id)
        {
            var category = GetCategoryById(id);
            if (category != null)
            {
                _context.Categorias.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }

        public List<Category> GetCategories(int pageNumber, int pageQuantity)
        {
            return _context
                .Categorias.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Include(c => c.Produtos)
                .ToList();
        }

        public Category GetCategoryById(Guid id)
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

        public Category PostCategory(Category category)
        {
            var newCategories = new Category { Nome = category.Nome };
            _context.Categorias.Add(newCategories);
            _context.SaveChanges();
            return newCategories;
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = GetCategoryById(category.Id);
            if (existingCategory != null)
            {
                existingCategory.Nome = category.Nome;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }
    }
}
