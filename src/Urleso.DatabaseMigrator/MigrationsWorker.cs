using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Urleso.Persistence;

namespace Urleso.DatabaseMigrator;

internal sealed class MigrationsWorker(
    IDbContextFactory<ApplicationDbContext> dbContextFactory,
    ILogger<MigrationsWorker> logger
)
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting migrations...");

        await using (var appDbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken))
        {
            await appDbContext.Database.MigrateAsync(cancellationToken);
        }

        logger.LogInformation("Migrations completed");
    }
}