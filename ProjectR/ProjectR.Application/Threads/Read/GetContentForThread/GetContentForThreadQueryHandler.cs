using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Threads.Read.GetContentForThread;

internal class GetContentForThreadQueryHandler : IQueryHandler<GetContentForThreadQuery, GetContentForThreadResponseDto>
{
    private IThreadRepository _threadRepository;
    public GetContentForThreadQueryHandler(IThreadRepository threadRepository)
    {
        _threadRepository = threadRepository;
    }
    public async Task<Result<GetContentForThreadResponseDto>> Handle(GetContentForThreadQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.Thread thread = await _threadRepository.GetThreadByIdAsync(request.threadId);

        if (thread is null)
        {
            return Result.Failure<GetContentForThreadResponseDto>(DomainErrors.Thread.ThreadIdNotFound(request.threadId));
        }

        return new GetContentForThreadResponseDto(thread.User.Username, thread.Title, thread.Content);
    }
}
