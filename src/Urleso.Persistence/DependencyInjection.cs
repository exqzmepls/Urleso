using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Urleso.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextFactory<ApplicationDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("POSTGRESQL");
            options.UseNpgsql(connectionString, o => o.MigrationsAssembly("Urleso.DatabaseMigrator"));
        });

        return services;
    }
}