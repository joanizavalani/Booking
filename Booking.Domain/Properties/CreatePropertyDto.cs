namespace Booking.Domain.Properties;

public record CreatePropertyDto
{
    public string Name { get; init; }

    public string Description { get; init; }

    public decimal PricePerNight { get; init; }

    public int MaxGuests { get; init; }

    public TimeOnly CheckInTime { get; init; }

    public TimeOnly CheckOutTime { get; init; }
}