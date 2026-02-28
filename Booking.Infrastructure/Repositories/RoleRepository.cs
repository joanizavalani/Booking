using Booking.Application.Features.Roles;
using Booking.Domain.Roles;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class RoleRepository
    : GenericRepository<Role>, IRoleRepository 
{
    public RoleRepository(BookingDbContext _dbContext)
        : base(_dbContext) { }

    public async Task<Role?> GetDefaultRoleAsync(CancellationToken cancellationToken)
    {
        return await _dbSet
            .FirstOrDefaultAsync(r => r.IsDefault, cancellationToken);
    }
}