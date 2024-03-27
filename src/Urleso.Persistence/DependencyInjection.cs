using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Urleso.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddOptions<ConnectionStrings>()
            .BindConfiguration(ConnectionStrings.ConfigurationSection)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddDbContextFactory<ApplicationDbContext>((serviceProvider, dbContextOptions) =>
        {
            var connectionStringsOptions = serviceProvider.GetRequiredService<IOptions<ConnectionStrings>>();
            var connectionString = connectionStringsOptions.Value.Postgresql;
            dbContextOptions.UseNpgsql(connectionString, npgsqlOptions =>
            {
                const string migrationsAssemblyName = "Urleso.DatabaseMigrator";
                npgsqlOptions.MigrationsAssembly(migrationsAssemblyName);
            });
        });

        return services;
    }
}