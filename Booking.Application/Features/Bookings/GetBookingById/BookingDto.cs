namespace Booking.Application.Features.Bookings;

public class BookingDto
{
    public Guid Id { get; set; }

    public Guid PropertyId { get; set; }

    public Guid GuestId { get; set; }


    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }


    public int GuestCount { get; set; }

    public decimal TotalPrice { get; set; }


    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastModifiedAt { get; set; }

    public DateTime? ConfirmedOn { get; set; }

    public DateTime? RejectedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public DateTime? CancelledOn { get; set; }
}