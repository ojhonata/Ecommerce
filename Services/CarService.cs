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
        private readonly ICloudinaryService _cloudinaryService;

        public CarService(ICarRepository carRepository, IMapper mapper, ICloudinaryService cloudinaryService)
        {
            _carRepository = carRepository;
            _mapper = mapper;
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

        public PageResponseDTO<CarDTO> FilterCars(CarFilterDTO filter)
        {
            var query = _carRepository.Query();

            if (!string.IsNullOrWhiteSpace(filter.Name))
                query = query.Where(c => c.Name.Contains(filter.Name));

            if (filter.PriceMin.HasValue)
                query = query.Where(c => c.Price >= filter.PriceMin.Value);

            if (filter.PriceMax.HasValue)
                query = query.Where(c => c.Price <= filter.PriceMax.Value);

            if (filter.YearMin.HasValue)
                query = query.Where(c => c.Year >= filter.YearMin.Value);

            if (filter.YearMax.HasValue)
                query = query.Where(c => c.Year <= filter.YearMax.Value);

            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId == filter.BrandId.Value);

            if (filter.CategoryId.HasValue)
                query = query.Where(c => c.CategoryId == filter.CategoryId.Value);

            var total = query.Count();

            var cars = query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            var dtoList = _mapper.Map<List<CarDTO>>(cars);

            return new PageResponseDTO<CarDTO>
            {
                Items = dtoList,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalItems = total
            };
        }
    }
}
