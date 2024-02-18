using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class StatusViewRepository : Repository<StatusView>, IStatusViewRepository
{
    public StatusViewRepository(AppDbContext context) : base(context)
    {
    }
}