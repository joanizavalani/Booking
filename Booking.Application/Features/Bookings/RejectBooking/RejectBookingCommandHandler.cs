using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Application.Features.Properties;
using Booking.Application.Features.Users;
using MediatR;

namespace Booking.Application.Features.Bookings.RejectBooking;

public class RejectBookingCommandHandler
    :IRequestHandler<RejectBookingCommand>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public RejectBookingCommandHandler(
        IBookingRepository bookingRepository,
        IPropertyRepository propertyRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService,
        IUnitOfWork unitOfWork)
    {
        _bookingRepository = bookingRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
    RejectBookingCommand command,
    CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user =
            await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var booking = await _bookingRepository
            .GetByIdAsync(command.BookingId, cancellationToken);

        if (booking == null)
            throw new NotFoundException("Booking not found.");

        var property = await _propertyRepository
            .GetByIdAsync(booking.PropertyId, cancellationToken);

        if (property.OwnerId != userId)
            throw new UnauthorizedException("You do not own this property.");

        booking.Reject();

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}