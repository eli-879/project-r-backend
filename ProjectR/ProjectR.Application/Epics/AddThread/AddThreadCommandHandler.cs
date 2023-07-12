using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Epics.AddThread;
internal class AddThreadCommandHandler : ICommandHandler<AddThreadCommand, AddThreadResponseDto>
{
    private readonly IEpicRepository _epicRepository;
    private readonly IUserRepository _userRepository;
    private readonly IThreadRepository _threadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddThreadCommandHandler(IEpicRepository epicRepository, IUserRepository userRepository, IThreadRepository threadRepository, IUnitOfWork unitOfWork)
    {
        _epicRepository = epicRepository;
        _userRepository = userRepository;
        _threadRepository = threadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AddThreadResponseDto>> Handle(AddThreadCommand request, CancellationToken cancellationToken)
    {
        bool isUserIdValid = Guid.TryParse(request.userId, out Guid parsedUserId);

        if (!isUserIdValid)
        {
            return Result.Failure<AddThreadResponseDto>(DomainErrors.User.UserIdIsNotValid(request.userId));
        }

        var epic = await _epicRepository.GetEpicByIdAsync(request.requestDto.epicId);
        var user = await _userRepository.GetUserByIdAsync(parsedUserId);

        if (epic is null)
        {
            return Result.Failure<AddThreadResponseDto>(DomainErrors.Epic.EpicIdNotFound(request.requestDto.epicId));
        }

        if (user is null)
        {
            return Result.Failure<AddThreadResponseDto>(DomainErrors.User.UserIdNotFound(parsedUserId));
        }

        var newThread = new Domain.Entities.Thread(
            Guid.NewGuid(),
            user.Id,
            epic.Id,
            request.requestDto.threadTitle,
            request.requestDto.threadText
            );

        _threadRepository.InsertThread(newThread);

        epic.Threads.Add(newThread);
        user.Threads.Add(newThread);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success(new AddThreadResponseDto("Success!"));
    }
}
