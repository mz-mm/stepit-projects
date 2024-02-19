using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    public Task<IEnumerable<Product>> GetAllWithStatusAsync();
}