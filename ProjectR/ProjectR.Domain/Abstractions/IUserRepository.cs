using ProjectR.Domain.Entities;

namespace ProjectR.Domain.Abstractions;
public interface IUserRepository
{

    Task<User?> GetUserByIdAsync(Guid id);

    Task<User?> GetUserByUsernameAsync(string name);
    void InsertUser(User user);

    void DeleteUser(User user);
}
