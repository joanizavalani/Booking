namespace Booking.Application.Contracts.Security;

public interface IPasswordHasher
{
    string HashPassword(string passwordToHash);
    bool VerifyPassword(string passwordToVerify, string userPassword);
}
