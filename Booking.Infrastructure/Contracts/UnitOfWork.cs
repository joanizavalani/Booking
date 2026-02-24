using Booking.Application.Contracts;

namespace Booking.Infrastructure.Contracts;

public class UnitOfWork
    : IUnitOfWork
{
    private readonly BookingDbContext _dbContext;
    public UnitOfWork(BookingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}