using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.OwnerProfiles;

public class OwnerProfile
{
    [Key]
    public Guid UserId { get; private set; }

    public string IdentityCardNumber { get; private set; }

    public bool VerificationStatus { get; private set; }

    public string? BusinessName { get; private set; }

    public string CreditCardNumber { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public DateTime LastModifiedAt { get; private set; }

    public User User { get; private set; }

    public OwnerProfile() { }

    public OwnerProfile(
        Guid userId,
        string identityCardNumber, 
        bool verificationStatus,
        string? businessName,
        string creditCardNumber,
        DateTime createdAt)
    {
        UserId = userId;
        IdentityCardNumber = identityCardNumber;
        VerificationStatus = verificationStatus;
        CreditCardNumber = creditCardNumber;
        CreatedAt = createdAt;
        LastModifiedAt = CreatedAt;
    }
}