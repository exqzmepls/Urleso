using Urleso.SharedKernel;

namespace Urleso.Application.Abstractions.Data;

public interface IUnitOfWork
{
    public Task<Result> SaveChangesAsync(CancellationToken cancellationToken);
}