namespace Booking.Application.Contracts.Security;

public interface ICurrentUserService
{
    Guid? UserId { get; }
}