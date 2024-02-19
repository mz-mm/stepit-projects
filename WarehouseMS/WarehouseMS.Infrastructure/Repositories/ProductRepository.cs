using Microsoft.EntityFrameworkCore;
using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }

    public virtual async Task<IEnumerable<Product>> GetAllWithStatusAsync() =>
        await _entities.AsNoTracking().Include(product => product.StatusView).ToListAsync();
}