using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Interfaces;

public interface IOrderProductRepository : IRepository<OrderProduct>
{
    public Task<IEnumerable<OrderProduct>> GetAllOrderProductByIdAsync(int orderId);
    public Task<bool> InsertOrderPorductAsync(int orderId, int productId);
}