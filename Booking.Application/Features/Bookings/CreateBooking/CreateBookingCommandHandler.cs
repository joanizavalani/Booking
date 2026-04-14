using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Application.Features.Properties;
using Booking.Application.Features.Users;
using Booking.Domain.Bookings;
using Booking.Domain.Properties;
using Booking.Domain.Users;
using MediatR;

namespace Booking.Application.Features.Bookings.CreateBooking;

public class CreateBookingCommandHandler
    : IRequestHandler<CreateBookingCommand, Guid>
{
    private readonly IBookingRepository _bookingRepository;
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBookingCommandHandler(
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

    public async Task<Guid> Handle(
    CreateBookingCommand command,
    CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user = await _userRepository
            .GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var property = await _propertyRepository
            .GetByIdAsync(command.BookingDto.PropertyId, cancellationToken);

        if (property == null || !property.IsActive)
            throw new NotFoundException("Property not found.");

        var hasConflict = await _bookingRepository.ExistsConfirmedOverlapAsync(
            property.Id,
            command.BookingDto.StartDate,
            command.BookingDto.EndDate,
            null,
            cancellationToken);

        if (hasConflict)
            throw new BadRequestException("Property already booked for selected dates.");

        var booking = BookingEntity.Create(
            property,
            user,
            command.BookingDto.StartDate,
            command.BookingDto.EndDate,
            command.BookingDto.GuestCount);

        await _bookingRepository.AddAsync(booking, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return booking.Id;
    }
}