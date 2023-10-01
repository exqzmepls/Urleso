using MediatR;
using Urleso.Domain.Shared;

namespace Urleso.Application.Messaging.Abstractions;

public interface IQuery<TResponse> : IRequest<TypedResult<TResponse>>;