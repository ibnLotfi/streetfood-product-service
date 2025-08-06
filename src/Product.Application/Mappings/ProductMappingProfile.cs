using AutoMapper;
using Product.Application.DTOs;

namespace Product.Application.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Domain.Entities.Product, ProductDto>()
            .ForMember(dest => dest.Category, 
                opt => opt.MapFrom(src => src.Category.ToString()))
            .ForMember(dest => dest.AvailableSizes, 
                opt => opt.MapFrom(src => src.AvailableSizes.Select(s => s.ToString()).ToList()));
    }
}