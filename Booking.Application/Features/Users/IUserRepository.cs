using Booking.Application.Contracts;
using Booking.Domain.Users;

namespace Booking.Application.Features.Users;

public interface IUserRepository : IGenericRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
