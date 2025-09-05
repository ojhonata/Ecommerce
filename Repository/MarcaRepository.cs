using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly AppDbContext _context;
        public MarcaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void DeleteMarca(Guid id)
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
            return _context.Marcas
                .Include(m => m.Produtos)
                .ToList();
        }

        public Marca ObterMarcaPorId(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentException("ID inválido.");
            }
            var marca = _context.Marcas.FirstOrDefault(m => m.Id == id);
            if (marca == null)
            {
                throw new Exception("Marca não encontrada.");
            }
            return marca;
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