using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Comments.Read;

public record GetCommentsForThreadQuery(Guid threadId) : IQuery<IEnumerable<GetCommentsForThreadResponseDto>>;

