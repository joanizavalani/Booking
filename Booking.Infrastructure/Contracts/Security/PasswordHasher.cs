using Booking.Application.Contracts.Security;

namespace Booking.Infrastructure.Contracts.Security;

public class PasswordHasher
    : IPasswordHasher
{
    public string HashPassword(string passwordToHash)
    {
        return BCrypt.Net.BCrypt.HashPassword(passwordToHash);
    }
    public bool VerifyPassword(string passwordToVerify, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(passwordToVerify, passwordHash);
    }
}