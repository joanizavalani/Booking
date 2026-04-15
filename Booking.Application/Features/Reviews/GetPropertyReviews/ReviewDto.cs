namespace Booking.Application.Features.Reviews.GetPropertyReviews;

public class ReviewDto
{
    public Guid Id { get; set; }

    public Guid GuestId { get; set; }

    public decimal Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime CreatedAt { get; set; }
}