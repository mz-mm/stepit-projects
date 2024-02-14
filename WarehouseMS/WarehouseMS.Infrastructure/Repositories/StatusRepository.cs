using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class StatusRepository : Repository<Status>, IStatusRepository
{
    public StatusRepository(AppDbContext context) : base(context)
    {
    }
}