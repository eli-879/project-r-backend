using Microsoft.EntityFrameworkCore;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;


namespace ProjectR.Infrastructure.Repositories;

public sealed class EpicRepository : IEpicRepository
{
    private readonly ApplicationDbContext _dbContext;
    public EpicRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void DeleteEpic(Epic epic)
    {
        _dbContext.Epics.Remove(epic);
    }

    public async Task<IEnumerable<Epic>> GetAllEpicsAsync()
    {
        IEnumerable<Epic> epics = await _dbContext.Epics.ToListAsync();
        return epics;
    }

    public async Task<Epic?> GetEpicByIdAsync(Guid epicId)
    {
        var epic = await _dbContext.Epics.FirstOrDefaultAsync(e => e.Id == epicId);

        return epic;
    }

    public void InsertEpic(Epic epic)
    {
        _dbContext.Epics.Add(epic);
    }
}
