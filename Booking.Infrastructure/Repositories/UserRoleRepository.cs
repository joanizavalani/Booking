using Booking.Application.Features.UserRoles;
using Booking.Domain.UserRoles;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories;

public class UserRoleRepository
    : GenericRepository<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(BookingDbContext dbContext)
        : base(dbContext) { }

    public async Task<List<UserRole>> GetAllUserRolesToListAsync(Guid userId)
    {
        return await _dbSet
            .Include(ur => ur.Role)
            .Where(ur => ur.UserId == userId)
            .ToListAsync();
    }
}