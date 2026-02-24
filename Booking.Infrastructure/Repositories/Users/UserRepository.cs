using Booking.Application.Features.Users;
using Booking.Domain.Users;
using Booking.Infrastructure.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Repositories.Users;

public class UserRepository
    : GenericRepository<User>, IUserRepository
{
    public UserRepository(BookingDbContext dbContext)
        : base(dbContext) { }

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await _dbSet.FirstOrDefaultAsync(
            (u => u.Email == email), cancellationToken);
    }
}
