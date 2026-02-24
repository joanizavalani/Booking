namespace Booking.Application.Contracts;

public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken);

    void Update(TEntity entity);

    void Delete(TEntity entity);
}