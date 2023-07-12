namespace ProjectR.Application.Epics.AddThread;

public record AddThreadRequestDto(string threadTitle, string threadText, Guid epicId);
