using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Errors;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Epics.Delete;
internal class DeleteEpicCommandHandler : ICommandHandler<DeleteEpicCommand>
{
    private readonly IEpicRepository _epicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteEpicCommandHandler(IEpicRepository epicRepository, IUnitOfWork unitOfWork)
    {
        _epicRepository = epicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteEpicCommand request, CancellationToken cancellationToken)
    {
        var epic = await _epicRepository.GetEpicByIdAsync(request.epicId);

        if (epic is null)
        {
            return Result.Failure(DomainErrors.Epic.EpicIdNotFound(request.epicId));
        }

        _epicRepository.DeleteEpic(epic);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();

    }
}
