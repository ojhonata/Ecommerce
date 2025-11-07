using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Data;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;

        public CarService(ICarRepository carRepository, IMapper mapper, IImageService imageService)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public List<CarDTO> GetCars(int pageNumber, int pageQuantity)
        {
            var cars = _carRepository.GetAll(pageNumber, pageQuantity);
            return _mapper.Map<List<CarDTO>>(cars);
        }

        public Car PostCar(CreateCarDTO car)
        {
            var urlImage = _imageService.ImageSave(car.Imagem);

            var newCar = _mapper.Map<Car>(car);
            newCar.Id = Guid.NewGuid();
            newCar.ImagemUrl = urlImage;

            _carRepository.PostCar(newCar);

            return newCar;
        }

        public Car GetCarById(Guid id)
        {
            var produto = _carRepository.GetById(id);
            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }
            return produto;
        }

        public Car PostCar(Car car)
        {
            if (string.IsNullOrEmpty(car.Nome))
            {
                throw new ArgumentException("O nome do carro é obrigatório.");
            }
            if (car.Preco <= 0)
            {
                throw new ArgumentException("O preço do carro deve ser maior que zero.");
            }
            if (car.Estoque < 0)
            {
                throw new ArgumentException("O estoque do carro não pode ser negativo.");
            }
            return _carRepository.Add(car);
        }

        public void DeleteCar(Guid id)
        {
            var produto = _carRepository.GetById(id);
            if (produto == null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }
            _carRepository.Remove(id);
        }

        public void UpdateProduto(Car car)
        {
            var existingCar = _carRepository.GetById(car.Id);
            if (existingCar == null)
            {
                throw new ArgumentException("Produto não encontrado.");
            }
            if (string.IsNullOrEmpty(car.Nome))
            {
                throw new ArgumentException("O nome do car é obrigatório.");
            }
            if (car.Preco <= 0)
            {
                throw new ArgumentException("O preço do car deve ser maior que zero.");
            }
            if (car.Estoque < 0)
            {
                throw new ArgumentException("O estoque do car não pode ser negativo.");
            }
            _carRepository.Update(car);
        }
    }
}
