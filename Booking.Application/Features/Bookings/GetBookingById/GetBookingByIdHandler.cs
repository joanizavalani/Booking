using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Application.Features.Properties;
using Booking.Application.Features.Users;
using MediatR;

namespace Booking.Application.Features.Bookings.GetBookingById;

public class GetBookingByIdQueryHandler
    : IRequestHandler<GetBookingByIdCommand, BookingDto>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetBookingByIdQueryHandler(
        IBookingRepository bookingRepository,
        IPropertyRepository propertyRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _bookingRepository = bookingRepository;
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<BookingDto> Handle(
        GetBookingByIdCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user = await _userRepository
            .GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var booking = await _bookingRepository
            .GetByIdAsync(command.BookingId, cancellationToken);

        if (booking == null)
            throw new NotFoundException("Booking not found.");

        var property = await _propertyRepository
            .GetByIdAsync(booking.PropertyId, cancellationToken);

        if (property == null)
            throw new NotFoundException("Property not found.");

        if (booking.GuestId != userId && property.OwnerId != userId)
            throw new UnauthorizedException(
                "You do not have access to this booking.");

        return new BookingDto
        {
            Id = booking.Id,
            PropertyId = booking.PropertyId,
            GuestId = booking.GuestId,
            StartDate = booking.StartDate,
            EndDate = booking.EndDate,
            GuestCount = booking.GuestCount,
            TotalPrice = booking.TotalPrice,
            Status = booking.Status.ToString(),
            CreatedAt = booking.CreatedAt,
            LastModifiedAt = booking.LastModifiedAt,
            ConfirmedOn = booking.ConfirmedOn,
            RejectedOn = booking.RejectedOn,
            CompletedOn = booking.CompletedOn,
            CancelledOn = booking.CancelledOn,
        };
    }
}