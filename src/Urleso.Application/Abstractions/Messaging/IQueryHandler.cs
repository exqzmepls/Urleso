using MediatR;
using Urleso.SharedKernel;

namespace Urleso.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TypedResult<TResponse>>
    where TQuery : IQuery<TResponse>;