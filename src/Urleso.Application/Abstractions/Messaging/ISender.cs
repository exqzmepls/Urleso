using Urleso.Domain.Results;

namespace Urleso.Application.Abstractions.Messaging;

public interface ISender
{
    public Task<Result> SendAsync(
        ICommand command,
        CancellationToken cancellationToken = default
    );

    public Task<TypedResult<TResponse>> SendAsync<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default
    );

    public Task<TypedResult<TResponse>> SendAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default
    );
}