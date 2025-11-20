using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public void DeleteCategory(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new ArgumentException("Category não encontrada.");
            }
            _categoryRepository.Remove(id);
        }

        public List<CategoryDTO> GetCategories(int pageNumber, int pageQuantity)
        {
            var categories = _categoryRepository.GetAll(pageNumber, pageQuantity);

            return _mapper.Map<List<CategoryDTO>>(categories);
        }

        public CategoryDTO GetCategoryById(Guid id)
        {
            var category = _categoryRepository.GetById(id);
            if (category == null)
            {
                throw new ArgumentException("Category não encontrada.");
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        public Category PostCategory(CategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new ArgumentException("O nome da category é obrigatório.");
            }
            var newCategory = new Category { Name = category.Name };
            return _categoryRepository.Add(newCategory);
        }

        public void UpdateCategory(Category category)
        {
            var existingCategory = _categoryRepository.GetById(category.Id);
            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                _categoryRepository.Update(existingCategory);
            }
            else
            {
                throw new ArgumentException("Category não encontrada.");
            }
        }
    }
}
