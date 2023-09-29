using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Threads.Read.GetContentForThread;

public record GetContentForThreadQuery(Guid threadId) : IQuery<GetContentForThreadResponseDto>;

