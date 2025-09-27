using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Interface
{
    public interface ICarService
    {
        public Car GetCarById(Guid id);
        public List<Car> GetCars();
        public Car PostCar(CarDTO car);
        void UpdateProduto(Car car);
        void DeleteCar(Guid id);
    }
}