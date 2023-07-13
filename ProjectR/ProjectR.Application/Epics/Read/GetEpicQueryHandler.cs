using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Epics.Read;
internal class GetEpicQueryHandler : IQueryHandler<GetEpicQuery, EpicResponseDto>
{
    private readonly IEpicRepository _epicRepository;

    public GetEpicQueryHandler(IEpicRepository epicRepository)
    {
        _epicRepository = epicRepository;

    }

    public async Task<Result<EpicResponseDto>> Handle(GetEpicQuery request, CancellationToken cancellationToken)
    {
        var epic = await _epicRepository.GetEpicByIdAsync(request.epicId);

        if (epic is null)
        {
            return Result.Failure<EpicResponseDto>(DomainErrors.Epic.EpicIdNotFound(request.epicId));
        }

        return new EpicResponseDto(epic.Name);
    }
}
