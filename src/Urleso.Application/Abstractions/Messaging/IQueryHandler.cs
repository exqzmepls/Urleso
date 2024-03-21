using MediatR;
using Urleso.Domain.Results;

namespace Urleso.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TypedResult<TResponse>>
    where TQuery : IQuery<TResponse>;