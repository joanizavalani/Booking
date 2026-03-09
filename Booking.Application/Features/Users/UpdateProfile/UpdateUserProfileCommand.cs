using MediatR;

namespace Booking.Application.Features.Users.UpdateProfile;

public record UpdateUserProfileCommand
    : IRequest<Guid>
{
    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? PhoneNumber { get; init; }
}