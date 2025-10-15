using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface ICarRepository
    {
        public Car GetCarById(Guid id);
        public List<Car> GetCars(int pageNumber, int pageQuantity);
        public Car PostProduto(CarDTO car);
        public Car PostCar(Car car);
        void UpdateProduto(Car car);
        void DeleteCar(Guid id);
    }
}