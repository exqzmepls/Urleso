using Urleso.Domain.Results;

namespace Urleso.Application.Abstractions.Messaging;

internal sealed class Sender(
    MediatR.ISender sender
) : ISender
{
    public async Task<Result> SendAsync(
        ICommand command,
        CancellationToken cancellationToken = default
    )
    {
        var response = await sender.Send(command, cancellationToken);
        return response;
    }

    public async Task<TypedResult<TResponse>> SendAsync<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default
    )
    {
        var response = await sender.Send(command, cancellationToken);
        return response;
    }

    public async Task<TypedResult<TResponse>> SendAsync<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default
    )
    {
        var response = await sender.Send(query, cancellationToken);
        return response;
    }
}