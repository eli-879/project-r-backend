using ProjectR.Domain.Primitives;

namespace ProjectR.Application.Epics.Read;
public record EpicResponseDto(string name, ICollection<ThreadDto> threads);

