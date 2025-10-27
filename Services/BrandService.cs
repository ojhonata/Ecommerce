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
        private readonly IWebHostEnvironment _env;
        private readonly IBrandRepository _brandRepository;

        public BrandService(IWebHostEnvironment env, IBrandRepository brandRepository)
        {
            _env = env;
            _brandRepository = brandRepository;
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

            _brandRepository.PostBrand(newBrand);

            return newBrand;
        }

        public void DeleteBrand(Guid id)
        {
            var brand = _brandRepository.GetBrandById(id);
            if (brand == null)
            {
                throw new ArgumentException("Marca não encontrada.");
            }
            _brandRepository.DeleteBrand(id);
        }

        public List<BrandDTO> GetBrands(int pageNumber, int pageQuantity)
        {
            var brands = _brandRepository.GetBrands(pageNumber, pageQuantity);
            var brandDtos = brands
                .Select(b => new BrandDTO
                {
                    Id = b.Id,
                    Nome = b.Nome,
                    ImagemURL = b.ImagemURL,
                })
                .ToList();

            return brandDtos;
        }

        public Brand GetBrandById(Guid id)
        {
            var brand = _brandRepository.GetBrandById(id);
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
            return _brandRepository.PostBrand(newBrand);
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
                throw new ArgumentException("Marca não encontrada.");
            }
        }
    }
}
