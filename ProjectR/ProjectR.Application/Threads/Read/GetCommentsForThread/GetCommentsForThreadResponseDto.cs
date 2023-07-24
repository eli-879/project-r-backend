using ProjectR.Domain.Entities;

namespace ProjectR.Application.Threads.Read.GetCommentsForThread;
public record GetCommentsForThreadResponseDto(string username, string message, ICollection<GetCommentsForThreadResponseDto> comments);
