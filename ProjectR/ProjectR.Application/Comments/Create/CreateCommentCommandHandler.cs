using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Comments.Create;

internal class CreateCommentCommandHandler : ICommandHandler<CreateCommentCommand, CreateCommentResponseDto>

{
    private readonly ICommentRepository _commentRepository;
    private readonly IUserRepository _userRepostiory;
    private readonly IThreadRepository _threadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCommentCommandHandler(ICommentRepository commentRepository, IUserRepository userRepostiory, IThreadRepository threadRepository, IUnitOfWork unitOfWork)
    {
        _commentRepository = commentRepository;
        _userRepostiory = userRepostiory;
        _unitOfWork = unitOfWork;
        _threadRepository = threadRepository;
    }

    public async Task<Result<CreateCommentResponseDto>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        bool isUserIdValid = Guid.TryParse(request.userId, out Guid parsedUserId);

        if (!isUserIdValid)
        {
            return Result.Failure<CreateCommentResponseDto>(DomainErrors.User.UserIdIsNotValid(request.userId));
        }

        User? user = await _userRepostiory.GetUserByIdAsync(parsedUserId);

        if (user is null)
        {
            return Result.Failure<CreateCommentResponseDto>(DomainErrors.User.UserIdNotFound(parsedUserId));
        }

        Domain.Entities.Thread? thread = await _threadRepository.GetThreadByIdAsync(request.requestDto.threadId);

        if (thread is null)
        {
            return Result.Failure<CreateCommentResponseDto>(DomainErrors.Thread.ThreadIdNotFound(request.requestDto.threadId));
        }

        Comment c = new Comment(Guid.NewGuid(), request.requestDto.message);
        Comment? commentParent = null;

        if (request.requestDto.parentCommentId is not null)
        {
            Guid parentCommentId = request.requestDto.parentCommentId.Value;
            commentParent = await _commentRepository.GetCommentByIdAsync(parentCommentId);

            if (commentParent is null)
            {
                return Result.Failure<CreateCommentResponseDto>(DomainErrors.Comment.CommentIdNotFound(request.requestDto.threadId));
            }
        }

        if (commentParent is not null)
        {
            commentParent.AddChildComment(c);
        }
        user.AddComment(c);
        thread.AddComment(c);

        await _commentRepository.InsertCommentAsync(c);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success(new CreateCommentResponseDto());
    }
}
