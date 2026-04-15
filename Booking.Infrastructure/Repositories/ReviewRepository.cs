using Booking.Application.Features.Properties;
using Booking.Domain.Properties;
using Booking.Domain.Reviews;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class ReviewRepository
    : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(BookingDbContext dbContext)
        : base(dbContext) { }

    public async Task<bool> ExistsByBookingIdAsync(Guid bookingId, CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .AnyAsync<Review>(r => r.BookingId == bookingId, cancellationToken);
    }

    public async Task<List<Review>> GetByPropertyIdAsync(
        Guid propertyId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Reviews
            .Where(r => r.Booking.PropertyId == propertyId)
            .ToListAsync(cancellationToken);
    }
}