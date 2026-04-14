using Booking.Domain.Bookings;
using MediatR;

namespace Booking.Application.Features.Bookings.CreateBooking;

public class CreateBookingCommand : IRequest<Guid>
{
    public CreateBookingDto BookingDto { get; init; }

    public CreateBookingCommand(CreateBookingDto bookingDto)
    {
        BookingDto = bookingDto;
    }
}