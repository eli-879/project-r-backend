using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Comments.Read;
internal class GetCommentsForThreadQueryHandler : IQueryHandler<GetCommentsForThreadQuery, IEnumerable<GetCommentsForThreadResponseDto>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IThreadRepository _threadRepository;

    public GetCommentsForThreadQueryHandler(ICommentRepository commentRepository, IUserRepository userRepository, IThreadRepository threadRepository)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _threadRepository = threadRepository;
    }

    public async Task<Result<IEnumerable<GetCommentsForThreadResponseDto>>> Handle(GetCommentsForThreadQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Comment> comments = await _commentRepository.GetCommentsFromThreadAsync(request.threadId);

        List<GetCommentsForThreadResponseDto> response = comments.Select(c => new GetCommentsForThreadResponseDto(c.User.Username, c.Message)).ToList();

        return response;
    }

}
