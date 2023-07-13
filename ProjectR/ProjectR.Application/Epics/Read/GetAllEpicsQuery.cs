using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Epics.Read;
public record GetAllEpicsQuery() : IQuery<IEnumerable<EpicResponseDto>>;

