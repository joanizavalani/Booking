using FluentValidation;

namespace Booking.Application.Features.Users.UpdateProfile;

public class UpdateUserProfileCommandValidator
    : AbstractValidator<UpdateUserProfileCommand>
{
    public UpdateUserProfileCommandValidator()
    {
        RuleFor(u => u.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(2).WithMessage("First name must be more than 2 characters.")
            .MaximumLength(52).WithMessage("First name cannot be larger than 52 characters.")
            .When(u => u.FirstName != null);

        RuleFor(u => u.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MinimumLength(2).WithMessage("Last name must be more than 2 characters.")
            .MaximumLength(52).WithMessage("Last name cannot be larger than 52 characters.")
            .When(u => u.LastName != null);

        RuleFor(u => u.PhoneNumber)
            .NotEmpty().WithMessage("Phone number is required.")
            .Matches(@"^\+?\d{10,15}$").WithMessage("Phone number must be valid.")
            .When(u => u.PhoneNumber != null);
    }
}