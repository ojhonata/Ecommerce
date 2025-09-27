using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public void DeleteBrand(Guid id)
        {
            var brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                throw new Exception("Marca não encontrada.");
            }
            _brandRepository.DeleteBrand(id);
        }

        public List<Brand> GetBrands()
        {
            return _brandRepository.GetBrands();
        }

        public Brand GetBrandById(Guid id)
        {
            var brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                throw new Exception("Marca não encontrada.");
            }
            return brand;
        }

        public Brand PostBrand(BrandDTO brand)
        {
            if (string.IsNullOrEmpty(brand.Nome) || string.IsNullOrEmpty(brand.ImagemURL))
            {
                throw new Exception("O nome e a URL da imagem da brand são obrigatórios.");
            }
            var newBrand = new Brand
            {
                Nome = brand.Nome,
                ImagemURL = brand.ImagemURL
            };
            return _brandRepository.PostBrand(brand);
        }

        public void UpdateBrand(Brand brand)
        {
            var existingBrand = _brandRepository.GetBrandById(brand.Id);
            if (existingBrand != null)
            {
                existingBrand.Nome = brand.Nome;
                existingBrand.ImagemURL = brand.ImagemURL;
                _brandRepository.UpdateBrand(existingBrand);
            }
            else
            {
                throw new Exception("Marca não encontrada.");
            }

        }
    }
}