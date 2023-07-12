using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Users.Delete;
public record DeleteUserCommand(Guid UserId) : ICommand;
