using Booking.Domain.Addresses;
using Booking.Domain.Properties;
using MediatR;

namespace Booking.Application.Features.Properties.CreateProperty;

public class CreatePropertyCommand
    : IRequest<Guid>
{
    public CreatePropertyDto CreatePropertyDto { get; set; }

    public string PropertyType { get; set; }

    public string Country { get; init; }

    public string City { get; init; }

    public string Street { get; init; }

    public string PostalCode { get; init; }

    public CreatePropertyCommand(
        CreatePropertyDto createPropertyDto,
        string propertyType,
        string country,
        string city,
        string street,
        string postalCode)
    {
        CreatePropertyDto = createPropertyDto;;
        PropertyType = propertyType;
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }
}