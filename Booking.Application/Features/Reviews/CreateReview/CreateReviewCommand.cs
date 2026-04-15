using Booking.Domain.Reviews;
using MediatR;

public class CreateReviewCommand
    : IRequest<Guid>
{
    public CreateReviewDto ReviewDto { get; set; }

    public CreateReviewCommand(CreateReviewDto reviewDto)
    {
        ReviewDto = reviewDto;
    }
}