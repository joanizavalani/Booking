using FluentValidation;

namespace Booking.Application.Features.Reviews.CreateReview;

public class CreateReviewCommandValidator
    : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.ReviewDto.BookingId)
            .NotEmpty().WithMessage("BookingId is required.");

        RuleFor(x => x.ReviewDto.Rating)
            .InclusiveBetween(1, 5) .WithMessage("Rating must be between 1 and 5.");

        RuleFor(x => x.ReviewDto.Rating)
            .Must(BeValidHalfStep)  .WithMessage("Rating must be in 0.5 increments.");

        RuleFor(x => x.ReviewDto.Comment)
            .MaximumLength(1000).WithMessage("Comment cannot exceed 1000 characters.")
            .When(x => x.ReviewDto.Comment != null);
    }

    private bool BeValidHalfStep(decimal rating)
    {
        return (rating * 2) % 1 == 0;
    }
}