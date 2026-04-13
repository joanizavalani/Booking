using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Exceptions;
using Booking.Application.Features.Addresses;
using Booking.Application.Features.Users;
using Booking.Domain.Addresses;
using Booking.Domain.Properties;
using MediatR;

namespace Booking.Application.Features.Properties.CreateProperty;

public class CreatePropertyCommandHandler
    : IRequestHandler<CreatePropertyCommand, Guid>
{
    private readonly IPropertyRepository _propertyRepository;
    private readonly IUserRepository _userRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserService _currentUserService;

    public CreatePropertyCommandHandler(
        IPropertyRepository propertyRepository,
        IUserRepository userRepository,
        IAddressRepository addressRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserService currentUserService)
    {
        _propertyRepository = propertyRepository;
        _userRepository = userRepository;
        _addressRepository = addressRepository;
        _unitOfWork = unitOfWork;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreatePropertyCommand command, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        if (userId == null)
            throw new UnauthorizedException("User is not authenticated.");

        var user =
            await _userRepository.GetByIdAsync(userId.Value, cancellationToken);

        if (user == null || !user.IsActive)
            throw new NotFoundException("User not found.");

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
            address = Address.AddNewAddress(
                country, city, street, postalCode);

            await _addressRepository.AddAsync(address, cancellationToken);
        }

        var propertyTypeString = command.PropertyType.Trim();

        if (!Enum.TryParse<PropertyType>(
            propertyTypeString, true, out var propertyType))
                throw new BadRequestException("Invalid property type.");

        var property = Property.CreateProperty(
            command.CreatePropertyDto,
            user.Id,
            address.Id,
            propertyType);

        await _propertyRepository.AddAsync(property, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return property.Id;
    }
}
