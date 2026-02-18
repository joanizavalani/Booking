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

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public int GuestCount { get; private set; }

    public decimal CleaningFee { get; private set; }

    public decimal AmenitiesUpCharge { get; private set; }

    public decimal PriceForPeriod { get; private set; }

    public decimal TotalPrice { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastModifiedAt { get; private set; }

    public DateTime? ConfirmedOn { get; private set; }

    public DateTime? RejectedOn { get; private set; }

    public DateTime?CompletedOn { get; private set; }

    public DateTime? CancelledOn { get; private set; }

    public Property Property { get; private set; }

    public User Guest { get; private set;  }

    public Review? Review { get; private set; }

    public BookingEntity() { }

    public BookingEntity(
        Property property,
        User guest,
        DateTime startDate,
        DateTime endDate,
        int guestCount,
        decimal cleaningFee,
        decimal amenitiesUpCharge,
        decimal priceForPeriod)
    {
        if (endDate <= startDate)
            throw new ArgumentException("End date must be after start date.");

        if (guestCount <= 0)
            throw new ArgumentOutOfRangeException("The number of guests can't be zero or less.");

        if (guestCount > property.MaxGuests)
            throw new ArgumentOutOfRangeException("The number of guests exceeds the expected amount for this property.");

        if (cleaningFee < 0 || amenitiesUpCharge < 0 || priceForPeriod < 0)
            throw new ArgumentException("An invalid amount of currency was added.");

        Id = Guid.NewGuid();

        Property = property;
        PropertyId = property.Id;

        Guest = guest;
        GuestId = guest.Id;

        StartDate = startDate;
        EndDate = endDate;

        CleaningFee = cleaningFee;
        AmenitiesUpCharge = amenitiesUpCharge;
        PriceForPeriod = priceForPeriod;
        TotalPrice = cleaningFee + amenitiesUpCharge + priceForPeriod;

        Status = BookingStatus.Pending;

        CreatedAt = DateTime.UtcNow;
        LastModifiedAt = CreatedAt;
    }
}