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
        public Car GetById(Guid id);
        public List<Car> GetAll(int pageNumber, int pageQuantity);
        public Car Add(Car car);
        public Car PostCar(Car car);
        void Update(Car car);
        void Remove(Guid id);
    }
}
