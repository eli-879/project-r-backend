using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Users.Login;
public record class LoginQuery(string username, string password) : IQuery<JWTResponseDto>;
