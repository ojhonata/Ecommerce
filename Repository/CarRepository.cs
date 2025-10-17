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

        public void DeleteCar(Guid id)
        {
            var car = GetCarById(id);
            if (car != null)
            {
                _context.Produtos.Remove(car);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }

        public List<Car> GetCars(int pageNumber, int pageQuantity)
        {
            return _context
                .Produtos.Skip((pageNumber - 1) * pageQuantity)
                .Take(pageQuantity)
                .Include(c => c.Marca)
                .ToList();
        }

        public Car GetCarById(Guid id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public Car PostProduto(CarDTO car)
        {
            var newCar = new Car
            {
                Nome = car.Nome,
                Preco = car.Preco,
                Descricao = car.Descricao,
                Estoque = car.Estoque,
                Ano = car.Ano,
                ImagemUrl = car.ImagemUrl,
                CategoriaId = Guid.Parse(car.CategoriaId.ToString()),
                MarcaId = Guid.Parse(car.MarcaId.ToString()),
            };

            _context.Produtos.Add(newCar);
            _context.SaveChanges();
            return newCar;
        }

        public Car PostCar(Car car)
        {
            _context.Produtos.Add(car);
            _context.SaveChanges();
            return car;
        }

        public void UpdateProduto(Car car)
        {
            var exisitngCar = _context.Produtos.Find(car.Id);
            if (exisitngCar != null)
            {
                exisitngCar.Nome = car.Nome;
                exisitngCar.Preco = car.Preco;
                exisitngCar.Descricao = car.Descricao;
                exisitngCar.Estoque = car.Estoque;
                exisitngCar.Ano = car.Ano;
                exisitngCar.ImagemUrl = car.ImagemUrl;
                exisitngCar.CategoriaId = car.CategoriaId;
                exisitngCar.MarcaId = car.MarcaId;

                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }
    }
}
