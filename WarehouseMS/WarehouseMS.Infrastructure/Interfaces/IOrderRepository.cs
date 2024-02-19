using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    public Task<IEnumerable<Order>> GetAllUserOrdersAsync(int userId);
    public Task<IEnumerable<Order>> GetAllWithOrderStatusAndProductsAndUserAsync();
    public Task<Order?> GetUserOrderByIdAsync(int userId, int orderId);
}