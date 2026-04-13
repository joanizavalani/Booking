using FluentValidation;

namespace Booking.Application.Features.Properties.UpdateProperty;

public class UpdatePropertyCommandValidator
    : AbstractValidator<UpdatePropertyCommand>
{
    public UpdatePropertyCommandValidator()
    {
        RuleFor(x => x.PropertyId)
            .NotEmpty().WithMessage("Property ID is required.");

        RuleFor(x => x.Name)
            .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
            .When(x => x.Name != null);

        RuleFor(x => x.Name)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Name cannot be empty.")
            .When(x => x.Name != null);

        RuleFor(x => x.Description)
            .MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.")
            .When(x => x.Description != null);

        RuleFor(x => x.Description)
            .Must(x => !string.IsNullOrWhiteSpace(x)) .WithMessage("Description cannot be empty.")
            .When(x => x.Description != null);

        RuleFor(x => x.PropertyType)
            .NotEmpty().WithMessage("Property type cannot be empty.")
            .When(x => x.PropertyType != null);

        RuleFor(x => x.PricePerNight)
            .GreaterThan(0).WithMessage("Price per night must be greater than 0.")
            .When(x => x.PricePerNight != null);

        RuleFor(x => x.MaxGuests)
            .GreaterThan(0).WithMessage("Max guests must be greater than 0.")
            .When(x => x.MaxGuests != null);

        RuleFor(x => x.Country)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Country cannot be empty.")
            .When(x => x.Country != null);

        RuleFor(x => x.City)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("City cannot be empty.")
            .When(x => x.City != null);

        RuleFor(x => x.Street)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Street cannot be empty.")
            .When(x => x.Street != null);

        RuleFor(x => x.PostalCode)
            .Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage("Postal code cannot be empty.")
            .When(x => x.PostalCode != null);

        RuleFor(x => x.CheckInTime)
            .NotNull().WithMessage("Check-in time cannot be null.")
            .When(x => x.CheckInTime != null);

        RuleFor(x => x.CheckOutTime)
            .NotNull().WithMessage("Check-out time cannot be null.")
            .When(x => x.CheckOutTime != null);
    }
}