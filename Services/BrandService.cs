using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Interface;
using Ecommerce.Models;

namespace Ecommerce.Services
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;    

        public BrandService(IBrandRepository brandRepository, IMapper mapper, IImageService imageService)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _imageService = imageService;
        }

        public Brand PostBrand(BrandImgDTO dto)
        {
            var url = _imageService.ImageSave(dto.Imagem);

            var newBrand = _mapper.Map<Brand>(dto);
            newBrand.Id = Guid.NewGuid();
            newBrand.ImagemURL = url;

            _brandRepository.Add(newBrand);

            return newBrand;
        }

        public void DeleteBrand(Guid id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                throw new ArgumentException("Marca não encontrada.");
            }
            _brandRepository.Delete(id);
        }

        public List<BrandDTO> GetBrands(int pageNumber, int pageQuantity)
        {
            var brands = _brandRepository.GetAll(pageNumber, pageQuantity);
            return _mapper.Map<List<BrandDTO>>(brands);
        }

        public Brand GetBrandById(Guid id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                throw new ArgumentException("Marca não encontrada.");
            }
            return brand;
        }

        public Brand PostBrand(BrandDTO brand)
        {
            if (string.IsNullOrEmpty(brand.Nome) || string.IsNullOrEmpty(brand.ImagemURL))
            {
                throw new ArgumentException("O nome e a URL da imagem da brand são obrigatórios.");
            }
            var newBrand = new BrandDTO { Nome = brand.Nome, ImagemURL = brand.ImagemURL };
            return _brandRepository.AddFromDTO(newBrand);
        }

        public void UpdateBrand(Brand brand)
        {
            var existingBrand = _brandRepository.GetById(brand.Id);
            if (existingBrand != null)
            {
                existingBrand.Nome = brand.Nome;
                existingBrand.ImagemURL = brand.ImagemURL;
                _brandRepository.Update(existingBrand);
            }
            else
            {
                throw new ArgumentException("Marca não encontrada.");
            }
        }
    }
}
