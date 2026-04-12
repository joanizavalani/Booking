using FluentValidation;

namespace Booking.Application.Features.Properties.CreateProperty;

public class CreatePropertyCommandValidator
    : AbstractValidator<CreatePropertyCommand>
{
    public CreatePropertyCommandValidator()
    {
        RuleFor(x => x.CreatePropertyDto.Name)
            .NotEmpty().WithMessage("Property name is required.")
            .MaximumLength(100).WithMessage("Property name is too long.");

        RuleFor(x => x.CreatePropertyDto.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(1000).WithMessage("Description is too long.");

        RuleFor(x => x.CreatePropertyDto.PricePerNight)
            .GreaterThan(0).WithMessage("Price per night must be greater than 0.");

        RuleFor(x => x.CreatePropertyDto.MaxGuests)
            .GreaterThan(0).WithMessage("Max guests must be greater than 0.");

        RuleFor(x => x.CreatePropertyDto.CheckInTime)
            .NotEmpty().WithMessage("Check-in time is required.");

        RuleFor(x => x.CreatePropertyDto.CheckOutTime)
            .NotEmpty().WithMessage("Check-out time is required.");

        RuleFor(x => x.PropertyType)
            .NotEmpty().WithMessage("Property type is required.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100);

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
            .MaximumLength(100);

        RuleFor(x => x.Street)
            .NotEmpty().WithMessage("Street is required.")
            .MaximumLength(150);

        RuleFor(x => x.PostalCode)
            .NotEmpty().WithMessage("Postal code is required.")
            .MaximumLength(20);
    }
}