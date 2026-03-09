using Booking.Application.Contracts.Security;
using System.Security.Claims;

namespace Booking.Api.Services;

public class CurrentUserService
    : ICurrentUserService
{
    private readonly IHttpContextAccessor _contextAccessor;

    public CurrentUserService(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var user = _contextAccessor.HttpContext?.User;

            if (user == null)
                return null;

            var userIdClaim =
                user.FindFirst(ClaimTypes.NameIdentifier)
                    ?? user.FindFirst("sub");

            if (userIdClaim == null)
                return null;

            if (Guid.TryParse(
                userIdClaim.Value, out var userId))
                    return userId;

            return null;
        }
    }

}