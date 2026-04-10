using Booking.Application.Features.Properties;
using Booking.Domain.Properties;
using Booking.Infrastructure.Contracts;

namespace Booking.Infrastructure.Repositories;

public class PropertyRepository
    : GenericRepository<Property>, IPropertyRepository
{
    public PropertyRepository(BookingDbContext _dbContext)
        : base(_dbContext) { }
}