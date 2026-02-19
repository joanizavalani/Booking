using Booking.Domain.Addresses;
using Booking.Domain.Bookings;
using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Properties;

public class Property
{
    [Key]
    public Guid Id { get; private set; }

    public Guid OwnerId { get; private set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public PropertyType PropertyType { get; private set; }

    public Guid AddressId { get; private set; }

    public int MaxGuests { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime CheckInTime { get; private set; }

    public DateTime CheckOutTime { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsApproved { get; private set; }

    public DateTime LastModifiedAt { get; private set; }

    public DateTime LastBookedOn { get; private set; }

    public User Owner { get; private set; }

    public Address Address { get; private set; }

    public List<BookingEntity> Bookings { get; private set; }

    public Property() { }

    public Property(
        Guid id,
        Guid ownerId,
        string name,
        string description,
        PropertyType propertyType,
        Guid addressId,
        int maxGuests,
        DateTime createdAt,
        DateTime checkInTime,
        DateTime checkOutTime,
        bool isActive,
        bool isApproved,
        DateTime lastModifiedAt,
        DateTime lastBookedOn)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Description = description;
        PropertyType = propertyType;
        AddressId = addressId;
        MaxGuests = maxGuests;
        CreatedAt = createdAt;
        CheckInTime = checkInTime;
        CheckOutTime = checkOutTime;
        IsActive = isActive;
        IsApproved = isApproved;
        LastModifiedAt = lastModifiedAt;
        LastBookedOn = lastBookedOn;

        Bookings = new List<BookingEntity>();
    }
}