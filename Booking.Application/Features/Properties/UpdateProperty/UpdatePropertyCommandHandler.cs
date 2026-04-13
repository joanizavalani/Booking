using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Application.Features.Addresses;
using Booking.Application.Features.Users;
using Booking.Domain.Addresses;
using Booking.Domain.Properties;
using MediatR;

namespace Booking.Application.Features.Properties.UpdateProperty;

public class UpdatePropertyCommandHandler
    : IRequestHandler<UpdatePropertyCommand, Guid>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IAddressRepository addressRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _propertyRepository = propertyRepository;
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(UpdatePropertyCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user =
            await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

        var property = await _propertyRepository.GetByIdAsync(command.PropertyId, cancellationToken);

        if (property == null)
            throw new NotFoundException("Property not found.");

        if (property.OwnerId != userId)
            throw new UnauthorizedException("You do not own this property.");

        Guid addressId = property.AddressId;

        if (command.Country != null &&
            command.City != null &&
            command.Street != null &&
            command.PostalCode != null)
        {
            var country = command.Country.Trim().ToLower();
            var city = command.City.Trim().ToLower();
            var street = command.Street.Trim().ToLower();
            var postalCode = command.PostalCode.Trim().ToLower();

            var existingAddress = await _addressRepository.GetByDetailsAsync(
                country, city, street, postalCode, cancellationToken);

            Address address;

            if (existingAddress != null)
                address = existingAddress;
            else
            {
                address = Address.AddNewAddress(country, city, street, postalCode);
                await _addressRepository.AddAsync(address, cancellationToken);
            }

            addressId = address.Id;
        }

        PropertyType propertyType;

        if (command.PropertyType == null)
            propertyType = property.PropertyType;
        else
        {
            var propertyTypeString = command.PropertyType.Trim();

            if (!Enum.TryParse<PropertyType>(
                propertyTypeString, true, out propertyType))
                    throw new BadRequestException("Invalid property type.");
        }

        property.Update(
            command.Name,
            command.Description,
            propertyType,
            command.PricePerNight,
            command.MaxGuests,
            command.CheckInTime,
            command.CheckOutTime,
            addressId);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return property.Id;
    }
}