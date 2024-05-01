using System.Data;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Urleso.Redirect.Persistence;

internal static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddOptions<ConnectionStrings>()
            .BindConfiguration(ConnectionStrings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.TryAddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();

        services.TryAddScoped<IDbConnection>(serviceProvider =>
        {
            var dbConnectionFactory = serviceProvider.GetRequiredService<IDbConnectionFactory>();
            return dbConnectionFactory.CreateConnection();
        });

        return services;
    }
}