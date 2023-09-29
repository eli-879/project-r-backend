using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Epics.Read;
public record GetEpicByIdQuery(Guid epicId) : IQuery<EpicResponseDto>;

