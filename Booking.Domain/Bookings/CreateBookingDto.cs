namespace Booking.Domain.Bookings;

public record CreateBookingDto
{
    public Guid PropertyId { get; init; }

    public DateOnly StartDate { get; init; }

    public DateOnly EndDate { get; init; }

    public int GuestCount { get; init; }
}