using Booking.Application;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Contracts;

public class GenericRepository<TEntity>
    : IGenericRepository<TEntity>
    where TEntity : class
{
    protected readonly BookingDbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(BookingDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}