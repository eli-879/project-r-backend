using MediatR;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Abstractions.Messaging;
public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
