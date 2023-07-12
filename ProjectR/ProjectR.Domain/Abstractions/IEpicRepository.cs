using ProjectR.Domain.Entities;

namespace ProjectR.Domain.Abstractions;

public interface IEpicRepository
{
    Task<Epic?> GetEpicByIdAsync(Guid epicId);

    void InsertEpic(Epic epic);

    void DeleteEpic(Epic epic);
}
