using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _marcaRepository;
        public MarcaService(IMarcaRepository marcaRepository)
        {
            _marcaRepository = marcaRepository;
        }
        public void DeleteMarca(int id)
        {
            var marca = _marcaRepository.ObterMarcaPorId(id);
            if (marca == null)
            {
                throw new Exception("Marca não encontrada.");
            }
            _marcaRepository.DeleteMarca(id);
        }

        public List<Marca> GetMarcas()
        {
            return _marcaRepository.GetMarcas();
        }

        public Marca ObterMarcaPorId(int id)
        {
            var marca = _marcaRepository.ObterMarcaPorId(id);
            if (marca == null)
            {
                throw new Exception("Marca não encontrada.");
            }
            return marca;
        }

        public Marca PostMarca(MarcaDTO marca)
        {
            if (string.IsNullOrEmpty(marca.Nome) || string.IsNullOrEmpty(marca.ImagemURL))
            {
                throw new Exception("O nome e a URL da imagem da marca são obrigatórios.");
            }
            var novaMarca = new Marca
            {
                Nome = marca.Nome,
                ImagemURL = marca.ImagemURL
            };
            return _marcaRepository.PostMarca(marca);
        }

        public void UpdateMarca(Marca marca)
        {
            var marcaExistente = _marcaRepository.ObterMarcaPorId(marca.Id);
            if (marcaExistente != null)
            {
                marcaExistente.Nome = marca.Nome;
                marcaExistente.ImagemURL = marca.ImagemURL;
                _marcaRepository.UpdateMarca(marcaExistente);
            }
            else
            {
                throw new Exception("Marca não encontrada.");
            }

        }
    }
}