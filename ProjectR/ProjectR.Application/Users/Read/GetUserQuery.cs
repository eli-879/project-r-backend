using MediatR;

namespace ProjectR.Application.Users.Read
{
    public record GetUserQuery(Guid userId) : IRequest<UserResponseDto>;

}
