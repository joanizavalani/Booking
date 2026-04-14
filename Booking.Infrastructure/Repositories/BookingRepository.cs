using Booking.Application.Features.Bookings;
using Booking.Domain.Bookings;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class BookingRepository
    : GenericRepository<BookingEntity>, IBookingRepository
{
    public BookingRepository(BookingDbContext dbContext)
        : base(dbContext) { }

    public async Task<bool> ExistsConfirmedOverlapAsync(
        Guid propertyId,
        DateOnly startDate,
        DateOnly endDate,
        Guid? excludeBookingId,
        CancellationToken cancellationToken)
    {
        return await _dbContext.Bookings.AnyAsync(b =>
            b.PropertyId == propertyId &&
            b.Status == BookingStatus.Confirmed &&
            (!excludeBookingId.HasValue || b.Id != excludeBookingId.Value) &&
            b.StartDate < endDate &&
            startDate < b.EndDate,
            cancellationToken);
    }
}
