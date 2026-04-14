using MediatR;

namespace Booking.Application.Features.Bookings.GetBookingById;

public class GetBookingByIdCommand
    : IRequest<BookingDto>
{
    public Guid BookingId { get; init; }

    public GetBookingByIdCommand(Guid bookingId)
    {
        BookingId = bookingId;
    }
}