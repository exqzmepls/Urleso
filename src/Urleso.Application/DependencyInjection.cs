using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Urleso.Application.Abstractions.Messaging;

namespace Urleso.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            var applicationAssembly = typeof(DependencyInjection).Assembly;
            configuration.RegisterServicesFromAssembly(applicationAssembly);
        });
        services.TryAddScoped<ISender, Sender>();

        return services;
    }
}