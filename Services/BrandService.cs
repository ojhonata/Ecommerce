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
        private readonly ICloudinaryService _cloudinaryService;

        public BrandService(
            IBrandRepository brandRepository,
            IMapper mapper,
            ICloudinaryService cloudinaryService
        )
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
        }

        public void DeleteBrand(Guid id)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
            {
                throw new ArgumentException("Brand not found.");
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
                throw new ArgumentException("Brand not found.");
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
            newBrand.ImageUrl = urlImage;
            newBrand.BgBrand = urlBgBrand;
            newBrand.LogoBrand = urlLogoBrand;

            _brandRepository.Add(newBrand);

            return newBrand;
        }

        public Brand UpdateBrand(Guid id, UpdateBrandDto dto)
        {
            var brand = _brandRepository.GetById(id);
            if (brand == null)
                throw new ArgumentException("Brand not found.");
            brand.Name = dto.Name;
            brand.Description = dto.Description;

            if (dto.Image != null)
                brand.ImageUrl = _cloudinaryService.UploadImage(dto.Image);

            if (dto.LogoBrand != null)
                brand.LogoBrand = _cloudinaryService.UploadImage(dto.LogoBrand);

            if (dto.BgBrand != null)
                brand.BgBrand = _cloudinaryService.UploadImage(dto.BgBrand);

            _brandRepository.Update(brand);

            return brand;
        }
    }
}
