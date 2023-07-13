using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Epics.Read;

internal class GetAllEpicsQueryHandler : IQueryHandler<GetAllEpicsQuery, IEnumerable<EpicResponseDto>>
{
    private readonly IEpicRepository _epicRepository;

    public GetAllEpicsQueryHandler(IEpicRepository epicRepository)
    {
        _epicRepository = epicRepository;
    }
    public async Task<Result<IEnumerable<EpicResponseDto>>> Handle(GetAllEpicsQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Epic> epics = await _epicRepository.GetAllEpicsAsync();

        List<EpicResponseDto> e = epics.Select(epic => new EpicResponseDto(epic.Name)).ToList();

        return e;
    }
}
