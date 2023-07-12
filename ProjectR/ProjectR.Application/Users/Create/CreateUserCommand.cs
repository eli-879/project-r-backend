using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Users.Create;
public record CreateUserCommand(string username, string email, string password) : ICommand<CreateUserResponseDto>;
