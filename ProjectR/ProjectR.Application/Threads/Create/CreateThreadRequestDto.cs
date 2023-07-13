namespace ProjectR.Application.Threads.Create;

public record CreateThreadRequestDto(string threadTitle, string threadText, Guid epicId);
