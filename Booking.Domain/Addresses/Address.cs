using Booking.Domain.Properties;
using System.ComponentModel.DataAnnotations;

namespace Booking.Domain.Addresses;

public class Address
{
    [Key]
    public Guid Id { get; private set; }

    public string Country { get; private set; }

    public string City { get; private set; }

    public string Street { get; private set; }

    public string PostalCode { get; private set; }

    public List<Property> Properties { get; private set; }

    private Address() { }

    private Address(
        Guid id,
        string country,
        string city,
        string street,
        string postalCode)
    {
        Id = id;
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;

        Properties = new List<Property>();
    }

    public static Address AddNewAddress(
        string country,
        string city,
        string street,
        string postalCode)
    {
        return new Address(
            id: Guid.NewGuid(),
            country: country,
            city: city,
            street: street,
            postalCode: postalCode);
    }
}