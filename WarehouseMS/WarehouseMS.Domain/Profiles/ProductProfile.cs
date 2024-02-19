using AutoMapper;
using WarehouseMS.Domain.Dtos.ProductDtos;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<CreateProductDto, Product>();
        CreateMap<Product, GetProductDto>();
        CreateMap<Product, GetProductWithStatusDto>()
            .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.StatusView.Id))
            .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusView.Name))
            .ForMember(dest => dest.StatusColor, opt => opt.MapFrom(src => src.StatusView.Color))
    }
}