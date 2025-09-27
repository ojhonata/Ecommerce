using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public void DeleteCategory(Guid id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            _categoryRepository.DeleteCategory(id);

        }

        public List<Category> GetCategories()
        {
            return _categoryRepository.GetCategories();
        }

        public Category GetCategoryById(Guid id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            return category;
        }

        public Category PostCategory(CategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.Nome))
            {
                throw new Exception("O nome da category é obrigatório.");
            }
            var newCategory = new Category
            {
                Nome = category.Nome
            };
            return _categoryRepository.PostCategory(newCategory);
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = _categoryRepository.GetCategoryById(category.Id);
            if (existingCategory != null)
            {
                existingCategory.Nome = category.Nome;
                _categoryRepository.UpdateCategory(existingCategory);
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }
    }
}