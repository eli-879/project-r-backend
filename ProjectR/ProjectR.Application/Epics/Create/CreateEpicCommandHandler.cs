using ProjectR.Application.Abstractions.Messaging;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;
using ProjectR.Domain.Shared;

namespace ProjectR.Application.Epics.Create;
internal class CreateEpicCommandHandler : ICommandHandler<CreateEpicCommand, CreateEpicResponseDto>
{
    private readonly IEpicRepository _epicRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateEpicCommandHandler(IEpicRepository epicRepository, IUnitOfWork unitOfWork)
    {
        _epicRepository = epicRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CreateEpicResponseDto>> Handle(CreateEpicCommand request, CancellationToken cancellationToken)
    {
        var epic = new Epic(Guid.NewGuid(), request.name);

        _epicRepository.InsertEpic(epic);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(new CreateEpicResponseDto(epic.Name));
    }
}
