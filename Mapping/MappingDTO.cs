using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Mapping
{
    public class MappingDTO : Profile
    {
        public MappingDTO()
        {
            CreateMap<Car, CarDTO>();
            CreateMap<CreateCarDTO, Car>();
            CreateMap<Brand, BrandDTO>();
            CreateMap<BrandImgDTO, Brand>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
