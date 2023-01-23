using Domain.Shared;
using MediatR;

namespace Outlou.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}