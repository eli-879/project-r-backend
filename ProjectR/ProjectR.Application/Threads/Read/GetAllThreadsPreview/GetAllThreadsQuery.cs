using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Application.Threads.Read.GetAllThreadsPreview;

namespace ProjectR.Application.Threads.Read.GetAllThreads;

public record GetAllThreadsPreviewQuery() : IQuery<ICollection<ThreadPreviewResponseDto>>;

