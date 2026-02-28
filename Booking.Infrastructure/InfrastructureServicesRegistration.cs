using Booking.Application.Contracts;
using Booking.Application.Contracts.Security;
using Booking.Application.Features.Roles;
using Booking.Application.Features.UserRoles;
using Booking.Application.Features.Users;
using Booking.Infrastructure.Contracts;
using Booking.Infrastructure.Contracts.Security;
using Booking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Booking.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection RegisterInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookingDbContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));

        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        var jwtSettings = configuration
            .GetSection("JwtSettings")
            .Get<JwtSettings>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
                    };
            });

        services.AddScoped(
            typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IPasswordHasher, PasswordHasher>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IRoleRepository, RoleRepository>();

        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        return services;
    }
}