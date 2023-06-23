using MediatR;

namespace ProjectR.Application.Users.Delete
{
    public record DeleteUserCommand(Guid UserId) : IRequest;


}
