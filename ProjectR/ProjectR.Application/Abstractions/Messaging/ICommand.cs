using MediatR;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}

