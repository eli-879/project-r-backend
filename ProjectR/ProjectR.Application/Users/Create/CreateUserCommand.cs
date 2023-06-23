using MediatR;

namespace ProjectR.Application.Users.Create
{
    public record CreateUserCommand(string username, string email, string password) : IRequest;

}
