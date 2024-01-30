using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    protected OrderRepository(AppDbContext context) : base(context)
    {
    }
}