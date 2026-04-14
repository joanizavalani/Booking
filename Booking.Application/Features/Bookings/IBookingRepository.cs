using Booking.Application.Contracts;
using Booking.Domain.Bookings;

namespace Booking.Application.Features.Bookings;

public interface IBookingRepository
    : IGenericRepository<BookingEntity>
{
    Task<bool> ExistsConfirmedOverlapAsync(
        Guid propertyId,
        DateOnly startDate,
        DateOnly endDate,
        Guid? excludeBookingId,
        CancellationToken cancellationToken);
}