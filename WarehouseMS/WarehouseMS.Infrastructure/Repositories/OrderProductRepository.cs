using Microsoft.EntityFrameworkCore;
using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class OrderProductRepository : Repository<OrderProduct>, IOrderProductRepository
{
    public OrderProductRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<OrderProduct>> GetAllOrderProductByIdAsync(int orderId) =>
        await _entities.AsNoTracking()
            .Where(op => op.OrderId == orderId)
            .Include(op => op.Product)
            .ToListAsync();

    public async Task<bool> InsertOrderProductAsync(int orderId, int productId)
    {
        var orderProduct = new OrderProduct
        {
            OrderId = orderId,
            ProductId = productId
        };

        await _entities.AddAsync(orderProduct);
        await base.SaveChangesAsync();

        return true;
    }
}