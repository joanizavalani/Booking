using Booking.Application.Contracts;
using Booking.Domain.Addresses;

namespace Booking.Application.Features.Addresses;

public interface IAddressRepository
    : IGenericRepository<Address>
{
    Task<Address?> GetByDetailsAsync(
        string country,
        string city,
        string street,
        string postalCode,
        CancellationToken cancellationToken);
}
