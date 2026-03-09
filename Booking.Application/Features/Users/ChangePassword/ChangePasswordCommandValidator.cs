using FluentValidation;

namespace Booking.Application.Features.Users.ChangePassword;

public class ChangePasswordCommandValidator
    :AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("Old password is required.");

        RuleFor(u => u.NewPassword)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"\d").WithMessage("Password must contain at least one number.")
            .Matches(@"[!@#$%^&*(),.?""':{}|<>]").WithMessage("Password must contain at least one special character.");
    }
}