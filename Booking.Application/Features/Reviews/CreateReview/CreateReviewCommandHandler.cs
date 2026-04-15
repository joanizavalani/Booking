using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Application.Features.Bookings;
using Booking.Application.Features.Users;
using Booking.Domain.Bookings;
using Booking.Domain.Reviews;
using MediatR;

public class CreateReviewCommandHandler
    : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateReviewCommandHandler(
        IBookingRepository bookingRepository,
        IReviewRepository reviewRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(
        CreateReviewCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User not authenticated.");

        var user = await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var booking = await _bookingRepository
            .GetByIdAsync(command.ReviewDto.BookingId, cancellationToken);

        if (booking == null)
            throw new NotFoundException("Booking not found.");

        if (booking.Status != BookingStatus.Completed)
            throw new BadRequestException("Only completed bookings can be reviewed.");

        if (booking.GuestId != user.Id)
            throw new UnauthorizedException("Only the guest can review this booking.");

        var exists = await _reviewRepository.ExistsByBookingIdAsync(
            booking.Id,
            cancellationToken);

        if (exists)
            throw new BadRequestException("Booking already reviewed.");

        var review = Review.Create(
            booking,
            user,
            command.ReviewDto.Rating,
            command.ReviewDto.Comment);

        await _reviewRepository.AddAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}