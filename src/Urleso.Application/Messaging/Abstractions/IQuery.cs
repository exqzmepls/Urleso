using MediatR;
using Urleso.Domain.Results;

namespace Urleso.Application.Messaging.Abstractions;

public interface IQuery<TResponse> : IRequest<TypedResult<TResponse>>;