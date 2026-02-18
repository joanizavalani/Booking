using Booking.Domain.Bookings;
using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Reviews;

public class Review
{
    [Key]
    public Guid Id { get; private set; }

    public Guid BookingId { get; private set; }

    public Guid GuestId { get; private set; }

    public decimal Rating { get; private set; }

    public string? Comment { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public BookingEntity Booking { get; private set; }

    public User Guest { get; private set; }

    public Review() { }

    public Review(Guid bookingId, Guid guestId, decimal rating)
    {
        ValidateRating(rating);

        Id = Guid.NewGuid();
        BookingId = bookingId;
        GuestId = guestId;
        Rating = rating;
        CreatedAt = DateTime.UtcNow;
    }

    public Review(Guid bookingId, Guid guestId, decimal rating, string comment)
    {
        ValidateRating(rating);

        Id = Guid.NewGuid();
        BookingId = bookingId;
        GuestId = guestId;
        Rating = rating;
        Comment = comment;
        CreatedAt = DateTime.UtcNow;
    }

    private void ValidateRating(decimal rating)
    {
        if (rating < 1 || rating > 5 || (2 * rating) % 1 != 0)
            throw new ArgumentException("Rating must be in 0.5 increments between 1 and 5 stars.");
    }
}