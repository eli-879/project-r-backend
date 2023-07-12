using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Epics.Read;
public record GetEpicQuery(Guid epicId) : IQuery<EpicResponseDto>;

