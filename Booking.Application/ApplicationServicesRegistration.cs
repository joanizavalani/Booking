using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;

namespace Booking.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection RegisterApplication
        (this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());

        return services;
    }
}
