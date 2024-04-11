namespace Urleso.Application.Abstractions.Data;

public interface IUnitOfWork
{
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}