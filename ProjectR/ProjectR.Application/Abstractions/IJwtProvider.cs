using ProjectR.Domain.Entities;

namespace ProjectR.Application.Abstractions
{
    public interface IJwtProvider
    {
        public string GenerateToken(User user);
    }
}
