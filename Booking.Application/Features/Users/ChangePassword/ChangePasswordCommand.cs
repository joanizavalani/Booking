using MediatR;

namespace Booking.Application.Features.Users.ChangePassword;

public record ChangePasswordCommand
    : IRequest<Guid>
{
    public string OldPassword { get; init; }

    public string NewPassword { get; init; }
}