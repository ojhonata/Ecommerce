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
        private readonly IWebHostEnvironment _env;
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;

        public BrandService(IWebHostEnvironment env, IBrandRepository brandRepository, IMapper mapper)
        {
            _env = env;
            _brandRepository = brandRepository;
            _mapper = mapper;
        }

        public Brand PostBrand(BrandImgDTO dto)
        {
            var pasta = Path.Combine(_env.WebRootPath, "imagens");
            if (!Directory.Exists(pasta))
                Directory.CreateDirectory(pasta);

            var nomeArquivo = Guid.NewGuid() + Path.GetExtension(dto.Imagem.FileName);
            var caminhoArquivo = Path.Combine(pasta, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                dto.Imagem.CopyTo(stream);
            }

            var url = $"/imagens/{nomeArquivo}";

            var newBrand = new Brand
            {
                Id = Guid.NewGuid(),
                Nome = dto.Nome,
                ImagemURL = url,
            };

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
