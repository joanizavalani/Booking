namespace Booking.Application.Contracts.Security;

public sealed record TokenResult
(
    string AccessToken,
    DateTime ExpiresAt
);