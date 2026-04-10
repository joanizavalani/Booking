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

    public string PasswordHash { get; private set; }

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

    private User() { }

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        string phoneNumber,
        string? profileImageUrl,
        bool isActive,
        DateTime createdAt)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
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

    public static User CreateUser(CreateUserDto dto, string passwordHash)
    {
        var id = Guid.NewGuid();

        if (!string.IsNullOrEmpty(dto.FirstName))
            throw new ArgumentException(
                "First name cannot be empty.");

        if (!string.IsNullOrEmpty(dto.FirstName))
            throw new ArgumentException(
                "Last name cannot be empty.");

        if (!string.IsNullOrEmpty(dto.PhoneNumber))
            throw new ArgumentException(
                "Phone number cannot be empty.");

        return new User(
            id: id,
            firstName: dto.FirstName,
            lastName: dto.LastName,
            email: dto.Email,
            passwordHash: passwordHash,
            phoneNumber: dto.PhoneNumber,
            profileImageUrl: null,
            isActive: true,
            createdAt: DateTime.UtcNow);
    }

    public void UpdateUser(string? firstName, string? lastName, string? phoneNumber)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
            FirstName = firstName;

        if (!string.IsNullOrWhiteSpace(lastName))
            LastName = lastName;

        if (!string.IsNullOrWhiteSpace(phoneNumber))
            PhoneNumber = phoneNumber;
    }

    public void UpdatePassword(string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
            throw new ArgumentException(
                "Password cannot be empty.");

        PasswordHash = hashedPassword;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void UpdateModificationTime()
    {
        LastModifiedAt = DateTime.UtcNow;
    }
}