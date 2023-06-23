using ProjectR.Domain.Abstractions;

namespace ProjectR.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellation)
        {
            return _dbContext.SaveChangesAsync(cancellation);
        }
    }
}
