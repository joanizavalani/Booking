using Booking.Application.Contracts;
using MediatR;

namespace Booking.Application.Features.Reviews.GetPropertyReviews;

public class GetPropertyReviewsQueryHandler
    : IRequestHandler<GetPropertyReviewsQuery, List<ReviewDto>>
{
    private readonly IReviewRepository _reviewRepository;

    public GetPropertyReviewsQueryHandler(
        IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task<List<ReviewDto>> Handle(
        GetPropertyReviewsQuery request,
        CancellationToken cancellationToken)
    {
        var reviews = await _reviewRepository
            .GetByPropertyIdAsync(request.PropertyId, cancellationToken);

        return reviews.Select(r => new ReviewDto
        {
            Id = r.Id,
            GuestId = r.GuestId,
            Rating = r.Rating,
            Comment = r.Comment,
            CreatedAt = r.CreatedAt
        }).ToList();
    }
}