using AutoMapper;
using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Domain.Resolvers;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Profiles;

public class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<Order, GetOrderDto>().ForMember(dest => dest.OrderStatus, opt => opt.MapFrom<StringToOrderStatusResolver>());
        CreateMap<CreateOrderDto, Order>();
    }
}