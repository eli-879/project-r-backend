using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Comments.Create;

public record CreateCommentCommand(CreateCommentRequestDto requestDto, string userId) : ICommand<CreateCommentResponseDto>;

