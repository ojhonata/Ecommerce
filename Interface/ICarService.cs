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
        public Car PostCarCloudinary(CreateCarDTO car);
        public Car GetCarById(Guid id);
        public List<CarDTO> GetCars(int pageNumber, int pageQuantity);
        void UpdateProduto(Car car);
        void DeleteCar(Guid id);
    }
}
