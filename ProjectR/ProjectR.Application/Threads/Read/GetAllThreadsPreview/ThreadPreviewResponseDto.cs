namespace ProjectR.Application.Threads.Read.GetAllThreadsPreview;

public record ThreadPreviewResponseDto(string epicName, string threadTitle, string threadDescription, int numComments);

