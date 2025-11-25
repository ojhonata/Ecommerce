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

        public CarService(
            ICarRepository carRepository,
            IMapper mapper,
            ICloudinaryService cloudinaryService
        )
        {
            _carRepository = carRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public List<CarDto> GetCars(int pageNumber, int pageQuantity)
        {
            var cars = _carRepository.GetAll(pageNumber, pageQuantity);
            return _mapper.Map<List<CarDto>>(cars);
        }

        public Car PostCarCloudinary(CreateCarDTO car)
        {
            var urlImage = _cloudinaryService.UploadImage(car.Image);
            var urlInnerImage = _cloudinaryService.UploadImage(car.InnerImage);
            var urlImageEngine = _cloudinaryService.UploadImage(car.ImageEngine);
            var urlVideoDemo = _cloudinaryService.UploadVideo(car.VideoDemoUrl);
            var urlModel3D = _cloudinaryService.UploadFile(car.Model3DUrl);

            var newCar = _mapper.Map<Car>(car);
            newCar.Id = Guid.NewGuid();
            newCar.ImageUrl = urlImage;
            newCar.InnerImageUrl = urlInnerImage;
            newCar.ImageEngineUrl = urlImageEngine;
            newCar.VideoDemoUrl = urlVideoDemo;
            newCar.Model3DUrl = urlModel3D;

            _carRepository.Add(newCar);
            return newCar;
        }

        public Car GetCarById(Guid id)
        {
            var produto = _carRepository.GetById(id);
            if (produto == null)
            {
                throw new ArgumentException("Car not found.");
            }
            return produto;
        }

        public void DeleteCar(Guid id)
        {
            var produto = _carRepository.GetById(id);
            if (produto == null)
            {
                throw new ArgumentException("Car not found.");
            }
            _carRepository.Remove(id);
        }

        public void UpdateCar(Guid id, UpdateCarDTO dto)
        {
            var existingCar = _carRepository.GetById(id);
            if (existingCar == null)
                throw new ArgumentException("Car not found.");

            if (!string.IsNullOrEmpty(dto.Name))
                existingCar.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Description))
                existingCar.Description = dto.Description;
            if (!string.IsNullOrEmpty(dto.Engine))
                existingCar.Engine = dto.Engine;

            if (dto.Price.HasValue)
            {
                if (dto.Price.Value <= 0)
                    throw new ArgumentException("Price > 0");
                existingCar.Price = dto.Price.Value;
            }

            if (dto.Stock.HasValue)
            {
                if (dto.Stock.Value < 0)
                    throw new ArgumentException("Stock cannot be negative");
                existingCar.Stock = dto.Stock.Value;
            }

            if (dto.Year.HasValue)
                existingCar.Year = dto.Year.Value;
            if (dto.Speed.HasValue)
                existingCar.Speed = dto.Speed.Value;

            if (dto.Image != null)
                existingCar.ImageUrl = _cloudinaryService.UploadImage(dto.Image);
            if (dto.InnerImage != null)
                existingCar.InnerImageUrl = _cloudinaryService.UploadImage(dto.InnerImage);
            if (dto.ImageEngine != null)
                existingCar.ImageEngineUrl = _cloudinaryService.UploadImage(dto.ImageEngine);
            if (dto.VideoDemoUrl != null)
                existingCar.VideoDemoUrl = _cloudinaryService.UploadVideo(dto.VideoDemoUrl);
            if (dto.Model3DUrl != null)
                existingCar.Model3DUrl = _cloudinaryService.UploadFile(dto.Model3DUrl);

            _carRepository.Update(existingCar);
        }

        public PageResponseDTO<CarDto> FilterCars(CarFilterDTO filter)
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

            var dtoList = _mapper.Map<List<CarDto>>(cars);

            return new PageResponseDTO<CarDto>
            {
                Items = dtoList,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize,
                TotalItems = total,
            };
        }
    }
}