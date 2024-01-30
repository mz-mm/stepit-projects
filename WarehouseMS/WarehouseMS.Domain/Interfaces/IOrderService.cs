using WarehouseMS.Domain.Dtos.OrderDtos;
using WarehouseMS.Domain.Enums;

namespace WarehouseMS.Domain.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync();
    Task<IEnumerable<GetOrderDto>> GetUserOrdersAsync(int userId);
    Task<GetOrderDto?> GetOrderbyIdAsync(int orderId);
    Task<GetOrderDto> CreateOrderAsync(CreateOrderDto orderDetails);
    Task<bool> UpdateOrderStatusAsync(int orderId, OrderStatus newStatus);
    Task<bool> UpdateOrderAsync(CreateOrderDto order);
    Task<bool> CancelOrderAsync(int orderId);
}