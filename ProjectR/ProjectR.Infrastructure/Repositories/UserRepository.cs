using Microsoft.EntityFrameworkCore;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;

namespace ProjectR.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;

        }

        public async Task<User?> GetUserByUsernameAsync(string name)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == name);

            return user;
        }

        public void InsertUser(User u)
        {
            _dbContext.Users.Add(u);
        }

        public void DeleteUser(User u)
        {
            _dbContext.Users.Remove(u);
        }


    }
}
