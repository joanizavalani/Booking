using MediatR;

namespace Booking.Application.Features.Users.UpdateProfile;

public class UpdateUserProfileCommand
    : IRequest<Guid>
{
    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public string? PhoneNumber { get; init; }
}