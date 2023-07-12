using MediatR;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Abstractions.Messaging;
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}
