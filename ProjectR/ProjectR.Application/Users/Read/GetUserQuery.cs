using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Users.Read;
public record GetUserQuery(Guid userId) : IQuery<UserResponseDto>;
