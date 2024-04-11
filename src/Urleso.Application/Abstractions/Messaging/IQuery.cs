using MediatR;
using Urleso.SharedKernel;

namespace Urleso.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<TypedResult<TResponse>>;