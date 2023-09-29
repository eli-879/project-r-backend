using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Application.Threads.Read.GetAllThreadsPreview;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Epics.Read;

internal class GetEpicByNameQueryHandler : IQueryHandler<GetEpicByNameQuery, EpicResponseDto>
{
    private readonly IEpicRepository _epicRepository;
    private readonly ICommentRepository _commentRepository;

    public GetEpicByNameQueryHandler(IEpicRepository epicRepository, ICommentRepository commentRepository)
    {
        _epicRepository = epicRepository;
        _commentRepository = commentRepository;
    }
    public async Task<Result<EpicResponseDto>> Handle(GetEpicByNameQuery request, CancellationToken cancellationToken)
    {
        var epic = await _epicRepository.GetEpicByNameAsync(request.epicName);

        if (epic is null)
        {
            return Result.Failure<EpicResponseDto>(DomainErrors.Epic.EpicNameNotFound(request.epicName));

        }

        List<ThreadDto> threads = new List<ThreadDto>();

        foreach (var thread in epic.Threads)
        {
            ICollection<Comment> comments = await _commentRepository.GetCommentsFromThreadAsync(thread.Id);

            threads.Add(new ThreadDto(thread.Title, thread.Content, thread.Id, comments.Count));

        }
        return new EpicResponseDto(epic.Name, threads);
    }
}
