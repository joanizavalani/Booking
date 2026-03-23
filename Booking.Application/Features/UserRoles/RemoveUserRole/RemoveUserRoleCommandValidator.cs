using FluentValidation;

namespace Booking.Application.Features.UserRoles.RemoveUserRole;

public class RemoveUserRoleCommandValidator
    : AbstractValidator<RemoveUserRoleCommand>
{
    public RemoveUserRoleCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.");

        RuleFor(x => x.RoleName)
            .NotEmpty().WithMessage("Role name is required.")
            .MaximumLength(50).WithMessage("Role name cannot exceed 50 characters.");
    }
}