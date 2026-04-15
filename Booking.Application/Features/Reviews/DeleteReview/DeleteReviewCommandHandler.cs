using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Domain.Reviews;
using MediatR;

namespace Booking.Application.Features.Reviews.DeleteReview;

public class DeleteReviewCommandHandler
    : IRequestHandler<DeleteReviewCommand>
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteReviewCommandHandler(
        IReviewRepository reviewRepository,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _reviewRepository = reviewRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        DeleteReviewCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var review = await _reviewRepository
            .GetByIdAsync(command.ReviewId, cancellationToken);

        if (review == null)
            throw new NotFoundException("Review not found.");

        if (review.GuestId != userId.Value)
            throw new UnauthorizedException(
                "You are not allowed to delete this review.");

        _reviewRepository.Delete(review);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}