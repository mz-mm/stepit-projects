using Microsoft.EntityFrameworkCore;
using WarehouseMS.Infrastructure.Context;
using WarehouseMS.Infrastructure.Context.Entities;
using WarehouseMS.Infrastructure.Interfaces;

namespace WarehouseMS.Infrastructure.Repositories;

public abstract class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;
    protected readonly DbSet<T> _entities;

    protected Repository(AppDbContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync() =>
        await _entities.AsNoTracking().ToListAsync();

    public virtual async Task<T?> GetByIdAsync(int id) =>
        await _entities.AsNoTracking().SingleOrDefaultAsync(s => s.Id == id);

    public virtual async Task<T> InsertAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await _entities.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<bool> UpdateAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _entities.Update(entity);
        await _context.SaveChangesAsync();

        return true;
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        _entities.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}