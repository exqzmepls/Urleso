using MediatR;
using Urleso.Domain.Shared;

namespace Urleso.Application.Messaging.Abstractions;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TypedResult<TResponse>>
    where TQuery : IQuery<TResponse>;