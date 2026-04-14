using FluentValidation;

namespace Booking.Application.Features.Bookings.CreateBooking;

public class CreateBookingCommandValidator
    : AbstractValidator<CreateBookingCommand>
{
    public CreateBookingCommandValidator()
    {
        RuleFor(x => x.BookingDto.PropertyId)
            .NotEmpty().WithMessage("PropertyId is required.");

        RuleFor(x => x.BookingDto.StartDate)
            .NotEmpty().WithMessage("Start date is required.");

        RuleFor(x => x.BookingDto.EndDate)
            .NotEmpty().WithMessage("End date is required.");

        RuleFor(x => x.BookingDto)
            .Must(x => x.EndDate > x.StartDate)
            .WithMessage("End date must be after start date.");

        RuleFor(x => x.BookingDto.GuestCount)
            .GreaterThan(0).WithMessage("Guest count must be greater than 0.");
    }
}