using Booking.Application.Contracts;
using Booking.Domain.Roles;

namespace Booking.Application.Features.Roles;

public interface IRoleRepository
    : IGenericRepository<Role>
{
    Task<Role?> GetDefaultRoleAsync(CancellationToken cancellationToken);
}