using WarehouseMS.Domain.Dtos.OrderDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<GetOrderDto>> GetAllOrdersAsync();
    Task<IEnumerable<GetOrdersWithStatusAndProductAndUserDto>> GetAllOrdersWithStatusAndProductsAsync();
    Task<IEnumerable<GetOrderDto>> GetUserOrdersAsync(int userId);
    Task<GetOrderDto?> GetUserOrderbyIdAsync(int userId, int orderId);
    Task<GetOrderDto?> GetOrderbyIdAsync(int orderId);
    Task<GetOrderDto> CreateOrderAsync(CreateOrderDto orderDetails);
    Task<bool> UpdateOrderStatusAsync(int orderId, int statusId);
    Task<bool> UpdateOrderAsync(CreateOrderDto order);
    Task<bool> DeleteOrderAsync(int orderId);
}