using Booking.Application.Exceptions;
using MediatR;

namespace Booking.Application.Features.Properties.GetProperty;

public class GetPropertyQueryHandler
    : IRequestHandler<GetPropertyCommand, PropertyDto>
{
    private readonly IPropertyRepository _propertyRepository;

    public GetPropertyQueryHandler(IPropertyRepository propertyRepository)
    {
        _propertyRepository = propertyRepository;
    }

    public async Task<PropertyDto> Handle(
        GetPropertyCommand command,
        CancellationToken cancellationToken)
    {
        var property = await _propertyRepository
            .GetByIdWithAddressAsync(command.PropertyId, cancellationToken);

        if (property == null)
            throw new NotFoundException("Property not found.");

        return new PropertyDto
        {
            Id = property.Id,
            Name = property.Name,
            Description = property.Description,
            PropertyType = property.PropertyType.ToString(),
            PricePerNight = property.PricePerNight,
            MaxGuests = property.MaxGuests,
            CheckInTime = property.CheckInTime,
            CheckOutTime = property.CheckOutTime,
            Country = property.Address.Country,
            City = property.Address.City,
            Street = property.Address.Street,
            PostalCode = property.Address.PostalCode
        };
    }
}
