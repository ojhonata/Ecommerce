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
    public class BrandRepository : IBrandRepository
    {
        private readonly AppDbContext _context;

        public BrandRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(Guid id)
        {
            var brand = GetById(id);
            if (brand != null)
            {
                _context.Marcas.Remove(brand);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Marca não encontrada.");
            }
        }

        public List<Brand> GetAll(int pageNumber, int pageQuantity)
        {
            return _context
                .Marcas.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Include(m => m.Produtos)
                .ToList();
        }

        public Brand GetById(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentException("ID inválido.");
            }
            var brand = _context.Marcas.FirstOrDefault(m => m.Id == id);
            if (brand == null)
            {
                throw new ArgumentException("Marca não encontrada.");
            }
            return brand;
        }

        public Brand AddFromDTO(BrandDTO brand)
        {
            var newBrand = new Brand { Nome = brand.Nome, ImagemURL = brand.ImagemURL };
            _context.Marcas.Add(newBrand);
            _context.SaveChanges();
            return newBrand;
        }

        public Brand Add(Brand brand)
        {
            _context.Marcas.Add(brand);
            _context.SaveChanges();
            return brand;
        }

        public void Update(Brand brand)
        {
            var existing = _context.Marcas.Find(brand.Id);
            if (existing != null)
            {
                throw new ArgumentException("Marca não encontrada.");
            }

            _context.Entry(existing).CurrentValues.SetValues(brand);
            _context.SaveChanges();

        }
    }
}
