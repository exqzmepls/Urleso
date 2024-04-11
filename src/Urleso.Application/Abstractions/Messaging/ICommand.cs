using MediatR;
using Urleso.SharedKernel;

namespace Urleso.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<TypedResult<TResponse>>;