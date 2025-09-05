using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public void DeleteCategoria(Guid id)
        {
            var categoria = _categoriaRepository.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            _categoriaRepository.DeleteCategoria(id);

        }

        public List<Categoria> GetCategorias()
        {
            return _categoriaRepository.GetCategorias();
        }

        public Categoria ObterCategoriaPorId(Guid id)
        {
            var categoria = _categoriaRepository.ObterCategoriaPorId(id);
            if (categoria == null)
            {
                throw new Exception("Categoria não encontrada.");
            }
            return categoria;
        }

        public Categoria PostCategoria(CategoriaDTO categoria)
        {
            if (string.IsNullOrEmpty(categoria.Nome))
            {
                throw new Exception("O nome da categoria é obrigatório.");
            }
            var novaCategoria = new Categoria
            {
                Nome = categoria.Nome
            };
            return _categoriaRepository.PostCategoria(novaCategoria);
        }

        public void UpdateCategoria(Categoria categoria)
        {
            var categoriaExistente = _categoriaRepository.ObterCategoriaPorId(categoria.Id);
            if (categoriaExistente != null)
            {
                categoriaExistente.Nome = categoria.Nome;
                _categoriaRepository.UpdateCategoria(categoriaExistente);
            }
            else
            {
                throw new Exception("Categoria não encontrada.");
            }
        }
    }
}