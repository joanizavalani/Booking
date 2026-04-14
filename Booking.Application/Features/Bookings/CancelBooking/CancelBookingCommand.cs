using MediatR;

namespace Booking.Application.Features.Bookings.CancelBooking;

public class CancelBookingCommand
    : IRequest
{
    public Guid BookingId { get; init; }

    public CancelBookingCommand(Guid bookingId)
    {
        BookingId = bookingId;
    }
}