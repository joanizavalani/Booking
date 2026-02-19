using Booking.Domain.Bookings;
using Booking.Domain.OwnerProfiles;
using Booking.Domain.Properties;
using Booking.Domain.Reviews;
using Booking.Domain.UserRoles;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Users;

public class User
{
    [Key]
    public Guid Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public string PhoneNumber { get; private set; }

    public string? ProfileImageUrl { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastModifiedAt { get; set; }

    public List<UserRole> UserRoles { get; private set; }

    public OwnerProfile? OwnerProfile { get; private set; }

    public List<Property> Properties { get; private set; }

    public List<BookingEntity> Bookings { get; private set; }

    public List<Review> Reviews { get; private set; }

    public User() { }

    public User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string password,
        string phoneNumber,
        string? profileImageUrl,
        bool isActive,
        DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        ProfileImageUrl = profileImageUrl;
        IsActive = true;
        CreatedAt = createdAt;
        LastModifiedAt = CreatedAt;

        UserRoles = new List<UserRole>();
        Properties = new List<Property>();
        Bookings = new List<BookingEntity>();
        Reviews = new List<Review>();
    }
}