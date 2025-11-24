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
                _context.Brands.Remove(brand);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Brand not found.");
            }
        }

        public List<Brand> GetAll(int pageNumber, int pageQuantity)
        {
            return _context
                .Brands.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Include(m => m.Cars)
                .ToList();
        }

        public Brand GetById(Guid id)
        {
            if (Guid.Empty == id)
            {
                throw new ArgumentException("ID invalid.");
            }
            var brand = _context.Brands.FirstOrDefault(m => m.Id == id);
            if (brand == null)
            {
                throw new ArgumentException("Brand not found.");
            }
            return brand;
        }

        public Brand AddFromDTO(BrandDTO brand)
        {
            var newBrand = new Brand { Name = brand.Name, ImageUrl = brand.ImageUrl };
            _context.Brands.Add(newBrand);
            _context.SaveChanges();
            return newBrand;
        }

        public Brand Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return brand;
        }

        public void Update(Brand brand)
        {
            var existing = _context.Brands.Find(brand.Id);
            if (existing != null)
            {
                throw new ArgumentException("Brand not found.");
            }

            _context.Entry(existing).CurrentValues.SetValues(brand);
            _context.SaveChanges();

        }
    }
}
