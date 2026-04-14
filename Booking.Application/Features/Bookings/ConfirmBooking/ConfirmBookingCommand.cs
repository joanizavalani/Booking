using MediatR;

namespace Booking.Application.Features.Bookings.ConfirmBooking;

public class ConfirmBookingCommand
    : IRequest
{
    public Guid BookingId { get; init; }

    public ConfirmBookingCommand(Guid bookingId)
    {
        BookingId = bookingId;
    }
}