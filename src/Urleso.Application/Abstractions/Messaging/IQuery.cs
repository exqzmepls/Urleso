using MediatR;
using Urleso.SharedKernel.Results;

namespace Urleso.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<TypedResult<TResponse>>;