using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Epics.Read;
internal class GetEpicByIdQueryHandler : IQueryHandler<GetEpicByIdQuery, EpicResponseDto>
{
    private readonly IEpicRepository _epicRepository;

    public GetEpicByIdQueryHandler(IEpicRepository epicRepository)
    {
        _epicRepository = epicRepository;

    }

    public async Task<Result<EpicResponseDto>> Handle(GetEpicByIdQuery request, CancellationToken cancellationToken)
    {
        var epic = await _epicRepository.GetEpicByIdAsync(request.epicId);

        if (epic is null)
        {
            return Result.Failure<EpicResponseDto>(DomainErrors.Epic.EpicIdNotFound(request.epicId));
        }

        ICollection<ThreadDto> threads = epic.Threads.Select(t => new ThreadDto(t.Title, t.Content, t.Id, 1)).ToList();

        return new EpicResponseDto(epic.Name, threads);
    }
}
