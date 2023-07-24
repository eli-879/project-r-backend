using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Threads.Read.GetCommentsForThread;

public record GetCommentsForThreadQuery(Guid threadId) : IQuery<IEnumerable<GetCommentsForThreadResponseDto>>;

