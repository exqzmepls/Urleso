using MediatR;
using Urleso.SharedKernel;

namespace Urleso.Application.Abstractions.Messaging;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TypedResult<TResponse>>
    where TCommand : ICommand<TResponse>;