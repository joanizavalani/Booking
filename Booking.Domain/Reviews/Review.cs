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

    private Review() { }

    private Review(
        Guid id,
        Guid bookingId,
        Guid guestId,
        decimal rating,
        string? comment,
        DateTime createdAt,
        BookingEntity booking)
    {
        if (rating < 1 || rating > 5 || (2 * rating) % 1 != 0)
            throw new ArgumentException("Rating must be in 0.5 increments between 1 and 5 stars.");

        Id = id;
        BookingId = bookingId;
        GuestId = guestId;
        Rating = rating;
        Comment = comment;
        CreatedAt = createdAt;
        Booking = booking;
    }

    public static Review Create(
        BookingEntity booking,
        User guest,
        decimal rating,
        string? comment)
    {
        if (booking.Status != BookingStatus.Completed)
            throw new InvalidOperationException(
                "You can only review completed bookings.");

        if (booking.GuestId != guest.Id)
            throw new UnauthorizedAccessException(
                "Only the booking guest can create a review.");

        return new Review(
            Guid.NewGuid(),
            booking.Id,
            guest.Id,
            rating,
            comment,
            DateTime.UtcNow,
            booking);
    }
}