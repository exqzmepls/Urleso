using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Urleso.Infrastructure.Data;
using Urleso.Infrastructure.Services;

namespace Urleso.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddData(configuration);
        services.AddServices();

        return services;
    }
}