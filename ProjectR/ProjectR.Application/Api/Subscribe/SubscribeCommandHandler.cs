﻿using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Enums;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Api.Subscribe;
public class SubscribeCommandHandler : ICommandHandler<SubscribeCommand>
{
    public IEpicRepository _epicRepository { get; set; }
    public IUserRepository _userRepository { get; set; }
    public IUnitOfWork _unitOfWork { get; set; }

    public SubscribeCommandHandler(IEpicRepository epicRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _epicRepository = epicRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(SubscribeCommand request, CancellationToken cancellationToken)
    {
        bool isUserIdValid = Guid.TryParse(request.userId, out Guid parsedUserId);

        if (!isUserIdValid)
        {
            return Result.Failure(DomainErrors.User.UserIdIsNotValid(request.userId));
        }

        var epic = await _epicRepository.GetEpicByIdAsync(request.requestDto.epicId);

        var user = await _userRepository.GetUserByIdAsync(parsedUserId);

        if (epic is null)
        {
            return Result.Failure(DomainErrors.Epic.EpicIdNotFound(request.requestDto.epicId));
        }

        if (user is null)
        {
            return Result.Failure(DomainErrors.User.UserIdNotFound(parsedUserId));
        }

        switch (request.requestDto.action)
        {
            case SubscribeActionsEnum.Sub:
                epic.Users.Add(user);
                break;

            case SubscribeActionsEnum.Unsub:
                epic.Users.Remove(user);
                break;

            default:
                return Result.Failure(new Error("a", "A"));
        }



        await _unitOfWork.SaveChangesAsync();

        return Result.Success();


    }
}
