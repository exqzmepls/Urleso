using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Urleso.DatabaseMigrator;

internal sealed class MigratorHostedService(
    IHostApplicationLifetime hostApplicationLifetime,
    MigrationsWorker migrationsWorker,
    ILogger<MigratorHostedService> logger
)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            try
            {
                await Task.Delay(Timeout.Infinite, hostApplicationLifetime.ApplicationStarted);
            }
            catch (OperationCanceledException)
            {
                await migrationsWorker.MigrateAsync(stoppingToken);
            }
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Error has occured while applying migrations");
            Environment.ExitCode = exception.HResult;
        }
        finally
        {
            hostApplicationLifetime.StopApplication();
        }
    }
}