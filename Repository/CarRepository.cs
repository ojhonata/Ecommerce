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
                _context.SaveChanges(); // salva as mudanças no banco de dados
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }

        // Em algum lugar como Data/CarRepository.cs
        // Certifique-se de ter este using!

        public List<Car> GetCars(int numberPage, int numberQuantity)
        {
            return _context.Produtos
                    .Skip((numberPage - 1) * numberQuantity)
                    .Take(numberQuantity)
                    .Include(c => c.Marca)
                    .ToList();
        }

        public Car GetCarById(Guid id)
        {
            return _context.Produtos.FirstOrDefault(p => p.Id == id); // retorna o car com o ID especificado
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

            _context.Produtos.Add(newCar); // adiciona o novo car ao contexto
            _context.SaveChanges(); // salva as mudanças no banco de dados
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

                _context.SaveChanges(); // salva as mudanças no banco de dados
            }
            else
            {
                throw new Exception("Produto não encontrado.");
            }
        }
    }
}