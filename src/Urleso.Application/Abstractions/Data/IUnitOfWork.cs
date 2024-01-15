using Urleso.Domain.Results;

namespace Urleso.Application.Abstractions.Data;

public interface IUnitOfWork
{
    public Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
}