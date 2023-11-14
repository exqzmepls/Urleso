using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Urleso.Application.Abstractions.Data;
using Urleso.Domain.Results;
using Urleso.Persistence;

namespace Urleso.Infrastructure.Data;

internal sealed class UnitOfWork(
        ApplicationDbContext dbContext,
        ILogger<UnitOfWork> logger
    )
    : IUnitOfWork
{
    public async Task<Result> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            logger.LogInformation("Saving changes...");
            await dbContext.SaveChangesAsync(cancellationToken);

            logger.LogInformation("Changes are saved");
            return Result.Success();
        }
        catch (DbUpdateException dbUpdateException)
        {
            logger.LogError(dbUpdateException, "An error is encountered while saving to the database");
            return Result.Failure(Errors.DbUpdateError);
        }
    }
}