using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly AppDbContext _context;
        public MarcaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void DeleteMarca(int id)
        {
            var marca = ObterMarcaPorId(id);
            if (marca != null)
            {
                _context.Marcas.Remove(marca);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Marca não encontrada.");
            }
        }

        public List<Marca> GetMarcas()
        {
            return _context.Marcas.ToList();
        }

        public Marca ObterMarcaPorId(int id)
        {
            return _context.Marcas.FirstOrDefault(m => m.Id == id);
        }

        public Marca PostMarca(MarcaDTO marca)
        {
            var novaMarca = new Marca
            {
                Nome = marca.Nome,
                ImagemURL = marca.ImagemURL

            };
            _context.Marcas.Add(novaMarca);
            _context.SaveChanges();
            return novaMarca;
        }

        public void UpdateMarca(Marca marca)
        {
            var marcaExistente = ObterMarcaPorId(marca.Id);
            if (marcaExistente != null)
            {
                marcaExistente.Nome = marca.Nome;
                marcaExistente.ImagemURL = marca.ImagemURL;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Marca não encontrada.");
            }
        }
    }
}