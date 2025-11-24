using AutoMapper;
using Ecommerce.DTOs;
using Ecommerce.Models;

namespace Ecommerce.Mapping
{
    public class MappingDTO : Profile
    {
        public MappingDTO()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CreateCarDTO, Car>();
            CreateMap<UpdateCarDTO, Car>();
            CreateMap<Brand, BrandDTO>();
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
            CreateMap<User, CreateUserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDTO, User>();
        }
    }
}
