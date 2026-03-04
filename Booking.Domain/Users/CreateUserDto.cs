namespace Booking.Domain.Users;

public record CreateUserDto
{
    public string FirstName { get; init; }

    public string LastName { get; init; }

    public string Email { get; init; }

    public string Password { get; init; }

    public string PhoneNumber { get; init; }

    public bool IsOwner { get; init; }
}