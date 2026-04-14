using MediatR;

public class RejectBookingCommand : IRequest
{
    public Guid BookingId { get; init; }
}