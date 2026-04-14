using Booking.Application.Contracts;
using Booking.Domain.Properties;

namespace Booking.Application.Features.Properties;

public interface IPropertyRepository
    : IGenericRepository<Property>
{
    Task<Property?> GetByIdWithAddressAsync(
        Guid propertyId,
        CancellationToken cancellationToken);

    Task<List<Property>> GetByOwnerIdWithAddressAsync(
        Guid ownerId,
        CancellationToken cancellationToken);
}