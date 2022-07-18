using AutoMapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.DTOs;

namespace CleanArchitecture.Mapper
{
    public class ProductMapperProfiles : Profile
    {
        public ProductMapperProfiles()
        {
            CreateMap<ProductDTO, Product>().ReverseMap();
        }
    }
}
