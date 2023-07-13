using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Threads.Create;

public record CreateThreadCommand(CreateThreadRequestDto requestDto, string userId) : ICommand<CreateThreadResponseDto>;
