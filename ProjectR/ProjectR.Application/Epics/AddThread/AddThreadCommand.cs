using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Epics.AddThread;

public record AddThreadCommand(AddThreadRequestDto requestDto, string userId) : ICommand<AddThreadResponseDto>;
