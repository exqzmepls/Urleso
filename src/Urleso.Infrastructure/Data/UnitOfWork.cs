using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Urleso.Application.Abstractions.Data;
using Urleso.Persistence;

namespace Urleso.Infrastructure.Data;

internal sealed class UnitOfWork(
    ApplicationDbContext dbContext,
    ILogger<UnitOfWork> logger
)
    : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Saving changes...");
            await dbContext.SaveChangesAsync(cancellationToken);
            logger.LogInformation("Changes are saved");
        }
        catch (DbUpdateException dbUpdateException)
        {
            logger.LogError(dbUpdateException, "An error is encountered while saving to the database");
            throw;
        }
    }
}