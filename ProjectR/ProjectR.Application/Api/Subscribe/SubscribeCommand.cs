using ProjectR.Application.Abstractions.Messaging;

namespace ProjectR.Application.Api.Subscribe;
public record SubscribeCommand(SubscribeRequestDto requestDto, string userId) : ICommand;


