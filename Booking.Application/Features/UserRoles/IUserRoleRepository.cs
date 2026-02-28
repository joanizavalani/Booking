using Booking.Application.Contracts;
using Booking.Domain.UserRoles;

namespace Booking.Application.Features.UserRoles;

public interface IUserRoleRepository
    : IGenericRepository<UserRole>
{
    Task<List<UserRole>> GetAllUserRolesToListAsync(Guid userId);
}