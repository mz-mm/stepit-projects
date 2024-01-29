using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    protected ProductRepository(AppDbContext context) : base(context)
    {
    }
}