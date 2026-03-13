using MediatR;

namespace Booking.Application.Features.UserRoles.AssignUserRole;

public record AssignUserRoleCommand
    : IRequest
{
    public Guid UserId { get; init; }

    public string RoleName { get; init; }
}