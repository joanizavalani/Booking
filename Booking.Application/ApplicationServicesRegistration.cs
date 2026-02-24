using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using MediatR;
using Booking.Application.Behaviors;

namespace Booking.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection RegisterApplication
        (this IServiceCollection services)
    {
        services.AddMediatR(
            cfg => cfg.RegisterServicesFromAssembly(
                Assembly.GetExecutingAssembly()));

        services.AddValidatorsFromAssembly(
            Assembly.GetExecutingAssembly());

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}
