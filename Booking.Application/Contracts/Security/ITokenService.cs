using Booking.Domain.UserRoles;
using Booking.Domain.Users;

namespace Booking.Application.Contracts.Security;

public interface ITokenService
{
    TokenResult GenerateToken(User user, List<UserRole> userRoles);
}