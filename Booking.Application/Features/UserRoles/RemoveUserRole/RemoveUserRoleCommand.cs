using MediatR;

namespace Booking.Application.Features.UserRoles.RemoveUserRole;

public record RemoveUserRoleCommand
    : IRequest
{
    public Guid UserId { get; init; }

    public string RoleName { get; init; }
}