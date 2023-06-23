using MediatR;

namespace ProjectR.Application.Users.Login
{
    public record class LoginQuery(string username, string password) : IRequest<JWTResponseDto>;

}
