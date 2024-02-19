using AutoMapper;
using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<CreateOrderDto, Order>();
        CreateMap<Order, GetOrderDto>();
        CreateMap<Order, GetOrdersWithStatusAndProductAndUserDto>()
            .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.Status.Status))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.OrderProducts.Select(op => op.Product)));
    }
}