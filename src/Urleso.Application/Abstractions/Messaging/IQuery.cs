using MediatR;
using Urleso.Domain.Results;

namespace Urleso.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<TypedResult<TResponse>>;