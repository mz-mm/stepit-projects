using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    protected UserRepository(AppDbContext context) : base(context)
    {
    }
}