using MediatR;

namespace Booking.Application.Features.Reviews.DeleteReview;

public class DeleteReviewCommand
    : IRequest
{
    public Guid ReviewId { get; set; }

    public DeleteReviewCommand(Guid reviewId)
    {
        ReviewId = reviewId;
    }
}