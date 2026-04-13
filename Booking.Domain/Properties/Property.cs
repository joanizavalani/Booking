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

    public decimal PricePerNight { get; private set; }

    public int MaxGuests { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public TimeOnly CheckInTime { get; private set; }

    public TimeOnly CheckOutTime { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsApproved { get; private set; }

    public DateTime LastModifiedAt { get; private set; }

    public DateTime? LastBookedOn { get; private set; }

    public User Owner { get; private set; }

    public Address Address { get; private set; }

    public List<BookingEntity> Bookings { get; private set; }

    private Property() { }

    private Property(
        Guid id,
        Guid ownerId,
        string name,
        string description,
        PropertyType propertyType,
        Guid addressId,
        decimal pricePerNight,
        int maxGuests,
        DateTime createdAt,
        TimeOnly checkInTime,
        TimeOnly checkOutTime,
        bool isActive,
        bool isApproved)
    {
        Id = id;
        OwnerId = ownerId;
        Name = name;
        Description = description;
        PropertyType = propertyType;
        AddressId = addressId;
        PricePerNight = pricePerNight;
        MaxGuests = maxGuests;
        CreatedAt = createdAt;
        CheckInTime = checkInTime;
        CheckOutTime = checkOutTime;
        IsActive = isActive;
        IsApproved = isApproved;
        LastModifiedAt = CreatedAt;
        LastBookedOn = null;

        Bookings = new List<BookingEntity>();
    }

    public static Property CreateProperty(
        CreatePropertyDto dto,
        Guid currentUserId,
        Guid addressId,
        PropertyType propertyType)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException(
                "Name cannot be empty.");

        if (dto.MaxGuests <= 0)
            throw new ArgumentException(
                "Max guests number cannot be 0 or less.");

        if (dto.PricePerNight < 0)
            throw new ArgumentException(
                "An invalid amount of currency was added.");

        if (currentUserId == Guid.Empty)
            throw new ArgumentException("OwnerId cannot be empty.");

        if (addressId == Guid.Empty)
            throw new ArgumentException("AddressId cannot be empty.");

        return new Property(
            id: Guid.NewGuid(),
            ownerId: currentUserId,
            name: dto.Name,
            description: dto.Description,
            propertyType: propertyType,
            addressId: addressId,
            pricePerNight: dto.PricePerNight,
            maxGuests: dto.MaxGuests,
            createdAt: DateTime.UtcNow,
            checkInTime: dto.CheckInTime,
            checkOutTime: dto.CheckOutTime,
            isActive: true,
            isApproved: true);
    }
    public void Update(
        string? name,
        string? description,
        PropertyType propertyType,
        decimal? pricePerNight,
        int? maxGuests,
        TimeOnly? checkInTime,
        TimeOnly? checkOutTime,
        Guid addressId)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");

            Name = name.Trim();
        }

        if (description != null)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Description cannot be empty.");

            Description = description.Trim();
        }

        if (pricePerNight != null)
        {
            if (pricePerNight <= 0)
                throw new ArgumentException("Price per night must be greater than 0.");

            PricePerNight = pricePerNight.Value;
        }

        if (maxGuests != null)
        {
            if (maxGuests <= 0)
                throw new ArgumentException("Max guests must be greater than 0.");

            MaxGuests = maxGuests.Value;
        }

        if (checkInTime != null)
            CheckInTime = checkInTime.Value;

        if (checkOutTime != null)
            CheckOutTime = checkOutTime.Value;

        PropertyType = propertyType;
        AddressId = addressId;
        LastModifiedAt = DateTime.UtcNow;
    }
}
