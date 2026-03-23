using Booking.Application.Contracts;
using Booking.Domain.UserRoles;

namespace Booking.Application.Features.UserRoles;

public interface IUserRoleRepository
    : IGenericRepository<UserRole>
{
    Task<UserRole?> GetByUserIdAndRoleIdAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);

    Task<List<UserRole>> GetAllUserRolesToListAsync(Guid userId, CancellationToken cancellationToken);

    Task<bool> ExistsAsync(Guid userId, Guid roleId, CancellationToken cancellationToken);
}