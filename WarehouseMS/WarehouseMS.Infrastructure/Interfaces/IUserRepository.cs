using WarehouseMS.Infrastructure.Context.Entities;

namespace WarehouseMS.Infrastructure.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}