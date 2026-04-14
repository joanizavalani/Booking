using MediatR;

namespace Booking.Application.Features.Bookings.CompleteBooking;

public class CompleteBookingCommand
    : IRequest
{
    public Guid BookingId { get; init; }

    public CompleteBookingCommand(Guid bookingId)
    {
        BookingId = bookingId;
    }
}