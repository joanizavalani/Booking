namespace Booking.Application.Features.Users.Login;

public record AuthResponse
(
    Guid UserId,
    string AccessToken,
    DateTime ExpiresAt
);
