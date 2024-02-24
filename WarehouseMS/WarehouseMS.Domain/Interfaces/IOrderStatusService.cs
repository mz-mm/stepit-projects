using WarehouseMS.Domain.Dtos.OrderStatusDtos;

namespace WarehouseMS.Domain.Interfaces;

public interface IOrderStatusService
{
    Task<IEnumerable<GetOrderStatusDto>> GetAllOrderStatusAsync();
    Task<GetOrderStatusDto?> GetOrderStatusByIdAsync(int orderStatusId);
    Task<GetOrderStatusDto> CreateOrderStatusAsync(CreateOrderStatusDto orderStatus);
    Task<bool> UpdateOrderStatusAsync(CreateOrderStatusDto orderStatus);
    Task<bool> DeleteOrderStatusAsync(int orderStatusId);
}