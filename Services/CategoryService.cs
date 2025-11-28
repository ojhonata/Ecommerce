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
                throw new ArgumentException("Category not found.");
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
                throw new ArgumentException("Category not found.");
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        public Category PostCategory(CategoryDTO category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new ArgumentException("The category name is required.");
            }
            var newCategory = new Category { Name = category.Name };
            return _categoryRepository.Add(newCategory);
        }

        public void UpdateCategory(Guid id, CategoryCreateDto category)
        {
            var existingCategory = _categoryRepository.GetById(id);

            if (existingCategory == null)
                throw new Exception("Category not found.");

            existingCategory.Name = category.Name;

            _categoryRepository.Update(existingCategory);
        }
    }
}
