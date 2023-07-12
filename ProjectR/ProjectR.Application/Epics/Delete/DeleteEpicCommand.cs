using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Epics.Delete;
public record DeleteEpicCommand(Guid epicId) : ICommand;
