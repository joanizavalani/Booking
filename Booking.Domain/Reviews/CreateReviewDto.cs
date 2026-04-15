namespace Booking.Domain.Reviews;

public class CreateReviewDto
{
    public Guid BookingId { get; set; }

    public decimal Rating { get; set; }

    public string? Comment { get; set; }
}