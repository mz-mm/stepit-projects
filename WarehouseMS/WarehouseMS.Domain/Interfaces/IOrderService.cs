﻿using WarehouseMS.Domain.Dtos.OrderDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync();
    Task<IEnumerable<GetOrdersWithProductAndUserDto>> GetAllOrdersWithStatusAndProductsAsync();
    Task<IEnumerable<GetOrderDto>> GetUserOrdersAsync(int userId);
    Task<GetOrderDto?> GetUserOrderByIdAsync(int userId, int orderId);
    Task<GetOrderDto?> GetOrderByIdAsync(int orderId);
    Task<GetOrderDto> CreateOrderAsync(CreateOrderDto orderDetails);
    Task UpdateOrderStatusAsync(GetOrdersWithProductAndUserDto orderDto);
    Task<bool> UpdateOrderAsync(CreateOrderDto order);
    Task<bool> DeleteOrderAsync(int orderId);
}