using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public List<Car> GetCars()
        {
            return _carRepository.GetCars();
        }

        public Car GetCarById(Guid id)
        {
            var produto = _carRepository.GetCarById(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado.");
            }
            return produto;
        }

        public Car PostCar(CarDTO car)
        {
            if (string.IsNullOrEmpty(car.Nome))
            {
                throw new Exception("O nome do car é obrigatório.");
            }
            if (car.Preco <= 0)
            {
                throw new Exception("O preço do car deve ser maior que zero.");
            }
            if (car.Estoque < 0)
            {
                throw new Exception("O estoque do car não pode ser negativo.");
            }
            return _carRepository.PostProduto(car);
        }

        public void DeleteCar(Guid id)
        {
            var produto = _carRepository.GetCarById(id);
            if (produto == null)
            {
                throw new Exception("Produto não encontrado.");
            }
            _carRepository.DeleteCar(id);   

        }

        public void UpdateProduto(Car car)
        {
            var existingCar = _carRepository.GetCarById(car.Id);
            if (existingCar == null)
            {
                throw new Exception("Produto não encontrado.");
            }
            if (string.IsNullOrEmpty(car.Nome))
            {
                throw new Exception("O nome do car é obrigatório.");
            }
            if (car.Preco <= 0)
            {
                throw new Exception("O preço do car deve ser maior que zero.");
            }
            if (car.Estoque < 0)
            {
                throw new Exception("O estoque do car não pode ser negativo.");
            }
            _carRepository.UpdateProduto(car);
        }
    }
}