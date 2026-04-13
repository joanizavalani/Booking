using MediatR;

namespace Booking.Application.Features.Properties.UpdateProperty;

public class UpdatePropertyCommand
    : IRequest<Guid>
{
    public Guid PropertyId { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }

    public string? PropertyType { get; init; }

    public int? MaxGuests { get; init; }

    public TimeOnly? CheckInTime { get; init; }

    public TimeOnly? CheckOutTime { get; init; }

    public decimal? PricePerNight { get; init; }

    public string? Country { get; init; }

    public string? City { get; init; }

    public string? Street { get; init; }

    public string? PostalCode { get; init; }
}