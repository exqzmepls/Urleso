using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Urleso.Persistence;

namespace Urleso.DatabaseMigrator;

internal sealed class MigratorHostedService(
        IDbContextFactory<ApplicationDbContext> dbContextFactory,
        ILogger<MigratorHostedService> logger
    )
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting migrations...");

        await using (var appDbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            await appDbContext.Database.MigrateAsync(cancellationToken);
        }

        logger.LogInformation("Migrations completed");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping Migrator...");
        return Task.CompletedTask;
    }
}