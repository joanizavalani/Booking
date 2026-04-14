using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Application.Features.Users;
using MediatR;

namespace Booking.Application.Features.Properties.GetMyProperties;

public class GetMyPropertiesQueryHandler
    : IRequestHandler<GetMyPropertiesCommand, List<PropertyDto>>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public GetMyPropertiesQueryHandler(
        IPropertyRepository propertyRepository,
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<List<PropertyDto>> Handle(
        GetMyPropertiesCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user =
            await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var properties = await _propertyRepository
            .GetByOwnerIdWithAddressAsync(userId.Value, cancellationToken);

        return properties.Select(p => new PropertyDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            PropertyType = p.PropertyType.ToString(),
            PricePerNight = p.PricePerNight,
            MaxGuests = p.MaxGuests,
            CheckInTime = p.CheckInTime,
            CheckOutTime = p.CheckOutTime,
            Country = p.Address.Country,
            City = p.Address.City,
            Street = p.Address.Street,
            PostalCode = p.Address.PostalCode
        }).ToList();
    }
}