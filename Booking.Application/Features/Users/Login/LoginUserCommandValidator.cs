using FluentValidation;

namespace Booking.Application.Features.Users.Login;

public class LoginUserCommandValidator
    : AbstractValidator<LoginUserCommand>
{
    public LoginUserCommandValidator()
    {
        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email address format.");

        RuleFor(u => u.Password)
            .NotEmpty().WithMessage("PasswordIsRequired.");
    }
}