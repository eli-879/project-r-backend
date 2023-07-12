using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Epics.Create;
public record CreateEpicCommand(string name) : ICommand<CreateEpicResponseDto>;

