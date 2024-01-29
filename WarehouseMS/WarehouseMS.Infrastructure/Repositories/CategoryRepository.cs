using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    protected CategoryRepository(AppDbContext context) : base(context)
    {
    }
}