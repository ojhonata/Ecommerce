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

        public List<CategoryDTO> GetCategories()
        {
            // 1. Busque as categorias e inclua os produtos e suas marcas no repositório
            //    Certifique-se que _categoryRepository.GetCategories() faz os .Include() corretos
            var categories = _categoryRepository.GetCategories();

            // 2. Mapeie para os DTOs, corrigindo o mapeamento do Carro
            var categoryDtos = categories.Select(cat => new CategoryDTO // <-- Variável renomeada para 'cat'
            {
                Id = cat.Id,
                Nome = cat.Nome,
                Produtos = cat.Produtos?.Select(prod => new CarDTO // <-- Variável renomeada para 'prod'
                {
                    // Mapeamento do Carro
                    Nome = prod.Nome,
                    Preco = prod.Preco,
                    Descricao = prod.Descricao,
                    Estoque = prod.Estoque,
                    Ano = prod.Ano,
                    ImagemUrl = prod.ImagemUrl,
                    CategoriaId = prod.CategoriaId,
                    MarcaId = prod.MarcaId,

                    // A PARTE ESSENCIAL QUE FALTAVA: Mapear a Marca do Carro
                    Marca = prod.Marca == null ? null : new BrandDTO
                    {
                        Nome = prod.Marca.Nome,
                        ImagemURL = prod.Marca.ImagemURL
                    }
                }).ToList()
            }).ToList();

            return categoryDtos;
        }

        // Altere a assinatura para retornar CategoryDTO
        public CategoryDTO GetCategoryById(Guid id)
        {
            // Busque a categoria com seus produtos e marcas no repositório
            var category = _categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                throw new Exception("Categoria não encontrada.");
            }

            // Mapeie para o DTO antes de retornar
            var categoryDto = new CategoryDTO
            {
                Nome = category.Nome,
                Produtos = category.Produtos?.Select(prod => new CarDTO
                {
                    // ... (mapeamento completo do CarDTO, incluindo a Marca)
                    Nome = prod.Nome,
                    Preco = prod.Preco,
                    Ano = prod.Ano,
                    ImagemUrl = prod.ImagemUrl,
                    MarcaId = prod.MarcaId,
                    Marca = prod.Marca == null ? null : new BrandDTO
                    {
                        Nome = prod.Marca.Nome,
                        ImagemURL = prod.Marca.ImagemURL
                    }
                }).ToList()
            };

            return categoryDto;
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