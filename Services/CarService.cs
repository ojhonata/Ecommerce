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
        private readonly IWebHostEnvironment _env;
        private readonly ICarRepository _carRepository;
        public CarService(IWebHostEnvironment env, ICarRepository carRepository)
        {
            _env = env;
            _carRepository = carRepository;
        }

        public List<CarDTO> GetCars()
        {
            var cars = _carRepository.GetCars();

            var carDtos = cars.Select(p => new CarDTO
            {
                Nome = p.Nome,
                Preco = p.Preco,
                Descricao = p.Descricao,
                Estoque = p.Estoque,
                Ano = p.Ano,
                ImagemUrl = p.ImagemUrl,
                CategoriaId = p.CategoriaId,
                MarcaId = p.MarcaId
            }).ToList();

            return carDtos;
        }

        public Car PostCar(CreateCarDTO car)
        {
            if (string.IsNullOrEmpty(car.Nome) || car.Imagem == null)
                throw new Exception("Nome e imagem são obrigatórios.");

            // pasta de armazenamento
            var pasta = Path.Combine(_env.WebRootPath, "imagens");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            // nome único para o arquivo
            var nomeArquivo = Guid.NewGuid() + Path.GetExtension(car.Imagem.FileName);
            var caminhoArquivo = Path.Combine(pasta, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                car.Imagem.CopyTo(stream);
            }

            // URL pública
            var url = $"/imagens/{nomeArquivo}";

            var newCar = new Car
            {
                Id = Guid.NewGuid(),
                Nome = car.Nome,
                Preco = car.Preco,
                Descricao = car.Descricao,
                Estoque = car.Estoque,
                Ano = car.Ano,
                ImagemUrl = url,
                CategoriaId = car.CategoriaId,
                MarcaId = car.MarcaId
            };

            _carRepository.PostCar(newCar);

            return newCar;
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