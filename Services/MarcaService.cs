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

        public List<string> GetMarcas()
        {
            List<Marca> marca = _marcaRepository.GetMarcas();
            List<string> marcas = new List<string>();

            foreach (var m in marca)
            {
                marcas.Add(m.Nome);
            }
            return marcas;
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
            if (string.IsNullOrEmpty(marca.Nome))
            {
                throw new Exception("O nome da marca é obrigatório.");
            }
            return _marcaRepository.PostMarca(marca);
        }

        public void UpdateMarca(Marca marca)
        {
            var marcaExistente = _marcaRepository.ObterMarcaPorId(marca.Id);
            if (marcaExistente == null)
            {
                throw new Exception("Marca não encontrada.");
            }
            _marcaRepository.UpdateMarca(marca);

        }
    }
}