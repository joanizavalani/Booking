using MediatR;

namespace Booking.Application.Features.Users.Login;

public record LoginUserCommand
    : IRequest<AuthResponse>
{
    public string Email { get; init; }

    public string Password { get; init; }
}