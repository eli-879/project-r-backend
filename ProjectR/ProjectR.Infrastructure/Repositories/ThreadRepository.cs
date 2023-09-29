using Microsoft.EntityFrameworkCore;
using ProjectR.Domain.Abstractions;

namespace ProjectR.Infrastructure.Repositories;

public class ThreadRepository : IThreadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ThreadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Domain.Entities.Thread>> GetAllThreadsAsync()
    {
        return await _dbContext.Threads.ToListAsync();
    }

    public async Task<Domain.Entities.Thread?> GetThreadByIdAsync(Guid threadId)
    {
        return await _dbContext.Threads.Include(t => t.User).FirstOrDefaultAsync(t => t.Id == threadId);
    }

    public void InsertThread(Domain.Entities.Thread thread)
    {
        _dbContext.Threads.Add(thread);
    }
}
