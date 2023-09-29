using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Application.Threads.Read.GetAllThreads;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Threads.Read.GetAllThreadsPreview;

internal class GetAllThreadsQueryHandler : IQueryHandler<GetAllThreadsPreviewQuery, ICollection<ThreadPreviewResponseDto>>
{
    private readonly IThreadRepository _threadRepository;
    private readonly IEpicRepository _epicRepository;
    private readonly ICommentRepository _commentRepository;

    public GetAllThreadsQueryHandler(IThreadRepository threadRepository, IEpicRepository epicRepository, ICommentRepository commentRepository)
    {
        _threadRepository = threadRepository;
        _epicRepository = epicRepository;
        _commentRepository = commentRepository;
    }
    public async Task<Result<ICollection<ThreadPreviewResponseDto>>> Handle(GetAllThreadsPreviewQuery request, CancellationToken cancellationToken)
    {
        List<ThreadPreviewResponseDto> response = new List<ThreadPreviewResponseDto>();
        IEnumerable<Domain.Entities.Thread> threads = await _threadRepository.GetAllThreadsAsync();

        foreach (var thread in threads)
        {
            Epic? epic = await _epicRepository.GetEpicByIdAsync(thread.EpicId);
            if (epic is null)
            {
                continue;
            }

            ICollection<Comment> comments = await _commentRepository.GetCommentsFromThreadAsync(thread.Id);

            string epicTitle = epic.Name;

            response.Add(new ThreadPreviewResponseDto(epicTitle, thread.Title, thread.Content, comments.Count));

        }

        return response;


    }
}
