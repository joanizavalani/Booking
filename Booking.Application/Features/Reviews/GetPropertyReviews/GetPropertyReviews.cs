using MediatR;

namespace Booking.Application.Features.Reviews.GetPropertyReviews;

public class GetPropertyReviewsQuery
    : IRequest<List<ReviewDto>>
{
    public Guid PropertyId { get; }

    public GetPropertyReviewsQuery(Guid propertyId)
    {
        PropertyId = propertyId;
    }
}