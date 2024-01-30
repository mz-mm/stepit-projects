using AutoMapper;
using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Domain.Enums;
using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Domain.Resolvers;

public class StringToOrderStatusResolver : IValueResolver<Order, GetOrderDto, OrderStatus>
{
    public OrderStatus Resolve(Order src, GetOrderDto dest, OrderStatus destMember, ResolutionContext context)
    {
        if (Enum.TryParse(src.OrderStatus, out OrderStatus result))
        {
            return result;
        }

        throw new InvalidOperationException($"Order status '{src.OrderStatus}' cannot be mapped to OrderStatus enum.");
    }

}