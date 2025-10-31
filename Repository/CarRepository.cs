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
    public class CarRepository : ICarRepository
    {
        private readonly AppDbContext _context;

        public CarRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Remove(Guid id)
        {
            var car = GetById(id);
            if (car != null)
            {
                _context.Produtos.Remove(car);
                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("Produto não encontrado.");
            }
        }

        public List<Car> GetAll(int pageNumber, int pageQuantity)
        {
            return _context
                .Produtos.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Include(c => c.Marca)
                .ToList();
        }

        public Car GetById(Guid id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public Car Add(Car car)
        {
            _context.Produtos.Add(car);
            _context.SaveChanges();
            return car;
        }

        public Car PostCar(Car car)
        {
            _context.Produtos.Add(car);
            _context.SaveChanges();
            return car;
        }

        public void Update(Car car)
        {
            var exisitngCar = _context.Produtos.Find(car.Id);
            if (exisitngCar != null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }
            
            _context.Entry(exisitngCar).CurrentValues.SetValues(car);
            _context.SaveChanges();
        }
    }
}
