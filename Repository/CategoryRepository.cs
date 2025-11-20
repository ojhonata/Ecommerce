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
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Category not found.");
            }
        }

        public List<Category> GetAll(int pageNumber, int pageQuantity)
        {
            return _context
                .Categories.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Include(c => c.Cars)
                .ToList();
        }

        public Category GetById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("ID invalid.");
            }
            var categoria = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
            {
                throw new ArgumentException("Category not found.");
            }
            return categoria;
        }

        public Category Add(Category category)
        {
            var newCategories = new Category { Name = category.Name };
            _context.Categories.Add(newCategories);
            _context.SaveChanges();
            return newCategories;
        }

        public void Update(Category category)
        {
            var existing = _context.Categories.Find(category.Id);
            if (existing != null)
            {
                throw new ArgumentException("Category not found.");
            }
            
            _context.Entry(existing).CurrentValues.SetValues(category);
            _context.SaveChanges();
        }
    }
}
