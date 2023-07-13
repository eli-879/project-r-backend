namespace ProjectR.Application.Comments.Create;
public record CreateCommentRequestDto(Guid threadId, Guid? parentCommentId, string message);

