using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Urleso.Redirect.Application;

internal static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.TryAddScoped<GetLongUrlQuery>();
        return services;
    }
}