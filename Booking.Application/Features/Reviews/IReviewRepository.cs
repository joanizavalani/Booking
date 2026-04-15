using Booking.Application.Contracts;
using Booking.Domain.Reviews;

public interface IReviewRepository
    : IGenericRepository<Review>
{
    Task<bool> ExistsByBookingIdAsync(
        Guid bookingId,
        CancellationToken cancellationToken);

    Task<List<Review>> GetByPropertyIdAsync(
        Guid propertyId,
        CancellationToken cancellationToken);
}