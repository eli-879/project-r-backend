using ProjectR.Domain.Entities;

namespace ProjectR.Domain.Abstractions;

public interface IEpicRepository
{
    Task<Epic?> GetEpicByIdAsync(Guid epicId);

    Task<Epic?> GetEpicByNameAsync(string epicName);

    Task<IEnumerable<Epic>> GetAllEpicsAsync();

    void InsertEpic(Epic epic);

    void DeleteEpic(Epic epic);
}
