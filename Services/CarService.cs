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
        private readonly ICloudinaryService _cloudinaryService;

        public CarService(ICarRepository carRepository, IMapper mapper, IImageService imageService, ICloudinaryService cloudinaryService)
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _imageService = imageService;
            _cloudinaryService = cloudinaryService;
        }

        public List<CarDTO> GetCars(int pageNumber, int pageQuantity)
        {
            var cars = _carRepository.GetAll(pageNumber, pageQuantity);
            return _mapper.Map<List<CarDTO>>(cars);
        }

        public Car PostCarCloudinary(CreateCarDTO car)
        {
            var urlImage = _cloudinaryService.UploadImage(car.Image);
            var urlInnerImage = _cloudinaryService.UploadImage(car.InnerImage);
            var urlImageEngine = _cloudinaryService.UploadImage(car.ImageEngine);
            var urlVideoDemo = _cloudinaryService.UploadVideo(car.VideoDemoUrl);

            var newCar = _mapper.Map<Car>(car);
            newCar.Id = Guid.NewGuid();
            newCar.ImageUrl = urlImage;
            newCar.InnerImageUrl = urlInnerImage;
            newCar.ImageEngineUrl = urlImageEngine;
            newCar.VideoDemoUrl = urlVideoDemo;

            _carRepository.Add(newCar);
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
            if (string.IsNullOrEmpty(car.Name))
            {
                throw new ArgumentException("O nome do car é obrigatório.");
            }
            if (car.Price <= 0)
            {
                throw new ArgumentException("O preço do car deve ser maior que zero.");
            }
            if (car.Stock < 0)
            {
                throw new ArgumentException("O estoque do car não pode ser negativo.");
            }
            _carRepository.Update(car);
        }
    }
}
