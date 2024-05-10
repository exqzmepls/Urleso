using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Urleso.Application.Abstractions.Services;

namespace Urleso.Infrastructure.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IRandomStringGenerator, CryptographyRandomStringGenerator>();
        services.TryAddSingleton<IClock, SystemClock>();

        return services;
    }
}