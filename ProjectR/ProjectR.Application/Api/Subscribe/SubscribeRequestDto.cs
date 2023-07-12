using ProjectR.Domain.Enums;

namespace ProjectR.Application.Api.Subscribe;

public record SubscribeRequestDto(
    SubscribeActionsEnum action,
    Guid epicId
    );

