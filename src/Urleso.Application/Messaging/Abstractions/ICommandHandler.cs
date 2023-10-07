using MediatR;
using Urleso.Domain.Shared;

namespace Urleso.Application.Messaging.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TypedResult<TResponse>>
    where TCommand : ICommand<TResponse>;