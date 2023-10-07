using MediatR;
using Urleso.Domain.Shared;

namespace Urleso.Application.Messaging.Abstractions;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<TypedResult<TResponse>>;