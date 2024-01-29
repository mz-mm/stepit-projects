using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class WarehouseRepository : Repository<Warehouse>, IWarehouseRepository
{
    protected WarehouseRepository(AppDbContext context) : base(context)
    {
    }
}