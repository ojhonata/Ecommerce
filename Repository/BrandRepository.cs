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

        public void DeleteBrand(Guid id)
        {
            var brand = GetBrandById(id);
            if (brand != null)
            {
                _context.Marcas.Remove(brand);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Marca não encontrada.");
            }
        }

        public List<Brand> GetBrands()
        {
            return _context.Marcas
                .Include(m => m.Produtos)
                .ToList();
        }

        public Brand GetBrandById(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentException("ID inválido.");
            }
            var brand = _context.Marcas.FirstOrDefault(m => m.Id == id);
            if (brand == null)
            {
                throw new Exception("Marca não encontrada.");
            }
            return brand;
        }

        public Brand PostBrand(BrandDTO brand)
        {
            var newBrand = new Brand
            {
                Nome = brand.Nome,
                ImagemURL = brand.ImagemURL

            };
            _context.Marcas.Add(newBrand);
            _context.SaveChanges();
            return newBrand;
        }

        public void UpdateBrand(Brand brand)
        {
            var brandExistente = GetBrandById(brand.Id);
            if (brandExistente != null)
            {
                brandExistente.Nome = brand.Nome;
                brandExistente.ImagemURL = brand.ImagemURL;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Marca não encontrada.");
            }
        }
    }
}