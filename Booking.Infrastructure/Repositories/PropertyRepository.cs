using Booking.Application.Features.Properties;
using Booking.Domain.Properties;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class PropertyRepository
    : GenericRepository<Property>, IPropertyRepository
{
    public PropertyRepository(BookingDbContext _dbContext)
        : base(_dbContext) { }

    public async Task<Property?> GetByIdWithAddressAsync(
        Guid propertyId,
        CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(p => p.Address)
            .FirstOrDefaultAsync(p => p.Id == propertyId, cancellationToken);
    }

    public async Task<List<Property>> GetByOwnerIdWithAddressAsync(
        Guid ownerId,
        CancellationToken cancellationToken)
    {
        return await _dbSet
            .Include(p => p.Address)
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync(cancellationToken);
    }
}