using ProjectR.Domain.Abstractions;

namespace ProjectR.Infrastructure.Repositories;

public class ThreadRepository : IThreadRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ThreadRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void InsertThread(Domain.Entities.Thread thread)
    {
        _dbContext.Threads.Add(thread);
    }
}
