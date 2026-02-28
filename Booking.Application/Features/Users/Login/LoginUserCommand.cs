using MediatR;

namespace Booking.Application.Features.Users.Login;

public class LoginUserCommand
    : IRequest<AuthResponse>
{
    public string Email { get; init; }

    public string Password { get; init; }

    public LoginUserCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}