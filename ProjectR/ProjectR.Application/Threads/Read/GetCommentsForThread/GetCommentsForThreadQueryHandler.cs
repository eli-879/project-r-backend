using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Threads.Read.GetCommentsForThread;
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

    public List<GetCommentsForThreadResponseDto> BuildCommentTree(Comment comment)
    {
        List<GetCommentsForThreadResponseDto> cc = new List<GetCommentsForThreadResponseDto>();

        foreach (Comment c in comment.ChildComments)
        {
            List<GetCommentsForThreadResponseDto> childComments = BuildCommentTree(c);
            cc.Add(new GetCommentsForThreadResponseDto(c.User.Username, c.Message, childComments));

        }

        return cc;

    }

    public async Task<Result<IEnumerable<GetCommentsForThreadResponseDto>>> Handle(GetCommentsForThreadQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Comment> comments = await _commentRepository.GetCommentsFromThreadAsync(request.threadId);
        List<GetCommentsForThreadResponseDto> commentTree = new List<GetCommentsForThreadResponseDto>();
        foreach (Comment comment in comments)
        {
            // hacky solution where I only consider comments that have no parent. May be a better solution? For some reason doesn't
            // work if change repository method to only return comments with no parent? Probably because it in the repo method it 
            // doesn't come with child mapping if I exclude those with parent comments...

            if (comment.CommentId != null) continue;
            commentTree.Add(new GetCommentsForThreadResponseDto(comment.User.Username, comment.Message, BuildCommentTree(comment)));

        }

        //List<GetCommentsForThreadResponseDto> response = comments.Select(c => new GetCommentsForThreadResponseDto(c.User.Username, c.Message, ).ToList();

        return commentTree;
    }

}
