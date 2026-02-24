using Booking.Application.Contracts;
using Booking.Application.Features.Users;
using Booking.Infrastructure.Contracts;
using Booking.Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingDbContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped(
            typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}