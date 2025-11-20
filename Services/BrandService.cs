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
        private readonly ICloudinaryService _cloudinaryService;

        public BrandService(IBrandRepository brandRepository, IMapper mapper, IImageService imageService, ICloudinaryService cloudinaryService)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _imageService = imageService;
            _cloudinaryService = cloudinaryService;
        }


        public void DeleteBrand(Guid id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                throw new ArgumentException("Brand não encontrada.");
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
                throw new ArgumentException("Brand não encontrada.");
            }
            return brand;
        }

        public Brand PostBrandCloudinary(CreateBrandDto dto)
        {
            var urlImage = _cloudinaryService.UploadImage(dto.Image);
            var urlBgBrand = _cloudinaryService.UploadImage(dto.BgBrand);
            var urlLogoBrand = _cloudinaryService.UploadImage(dto.LogoBrand);

            var newBrand = _mapper.Map<Brand>(dto);

            newBrand.Id = Guid.NewGuid();
            newBrand.ImageURL = urlImage;
            newBrand.BgBrand = urlBgBrand;
            newBrand.LogoBrand = urlLogoBrand;

            _brandRepository.Add(newBrand);

            return newBrand;

        }

        public void UpdateBrand(Brand brand)
        {
            var existingBrand = _brandRepository.GetById(brand.Id);
            if (existingBrand != null)
            {
                existingBrand.Name = brand.Name;
                existingBrand.ImageURL = brand.ImageURL;
                _brandRepository.Update(existingBrand);
            }
            else
            {
                throw new ArgumentException("Brand não encontrada.");
            }
        }
    }
}
