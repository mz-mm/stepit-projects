using Microsoft.EntityFrameworkCore;
using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Order>> GetAllUserOrdersAsync(int userId) =>
        await _entities
            .AsNoTracking()
            .Where(order => order.UserId == userId)
            .ToListAsync();

    public async Task<IEnumerable<Order>> GetAllWithProductsAndUserAsync() =>
        await _entities
            .AsNoTracking()
            .Include(x => x.Status)
            .Include(x => x.User)
            .Include(order => order.OrderProducts)
            .ThenInclude(orderProduct => orderProduct.Product)
            .ToListAsync();

    public async Task<Order?> GetUserOrderByIdAsync(int userId, int orderId) =>
        await _entities
            .AsNoTracking()
            .SingleOrDefaultAsync(order => order.UserId == userId && order.Id == orderId);
}