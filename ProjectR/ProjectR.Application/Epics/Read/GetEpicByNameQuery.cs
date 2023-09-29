using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Epics.Read;

public record GetEpicByNameQuery(string epicName) : IQuery<EpicResponseDto>;
