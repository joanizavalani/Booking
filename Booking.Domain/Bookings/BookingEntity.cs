using Booking.Domain.Properties;
using Booking.Domain.Reviews;
using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Bookings;

public class BookingEntity
{
    [Key]
    public Guid Id { get; private set; }

    public Guid PropertyId { get; private set; }

    public Guid GuestId { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly EndDate { get; private set; }

    public int GuestCount { get; private set; }

    public decimal TotalPrice { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastModifiedAt { get; private set; }

    public DateTime? ConfirmedOn { get; private set; }

    public DateTime? RejectedOn { get; private set; }

    public DateTime? CompletedOn { get; private set; }

    public DateTime? CancelledOn { get; private set; }

    public Property Property { get; private set; }

    public User Guest { get; private set;  }

    public Review? Review { get; private set; }

    private BookingEntity() { }

    private BookingEntity(
        Guid id,
        Property property,
        User guest,
        DateOnly startDate,
        DateOnly endDate,
        int guestCount,
        BookingStatus status,
        DateTime createdAt,
        DateTime? confirmedOn,
        DateTime? rejectedOn,
        DateTime? completedOn,
        DateTime? cancelledOn,
        Review? review)
    {
        if (endDate <= startDate)
            throw new ArgumentException(
                "End date must be after start date.");

        if (guestCount <= 0)
            throw new ArgumentOutOfRangeException(
                "The number of guests can't be zero or less.");

        if (guestCount > property.MaxGuests)
            throw new ArgumentOutOfRangeException(
                "The number of guests exceeds the expected amount for this property.");

        Id = id;

        Property = property;
        PropertyId = property.Id;

        Guest = guest;
        GuestId = guest.Id;

        StartDate = startDate;
        EndDate = endDate;

        var nights = endDate.DayNumber - startDate.DayNumber;
        TotalPrice = property.PricePerNight * nights;

        GuestCount = guestCount;
        Status = status;

        CreatedAt = createdAt;
        LastModifiedAt = CreatedAt;
        
        ConfirmedOn = confirmedOn;
        RejectedOn = rejectedOn;
        CompletedOn = completedOn;
        CancelledOn = cancelledOn;

        Review = review;
    }
}