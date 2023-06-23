using Microsoft.EntityFrameworkCore;
using ProjectR.Domain.Entities;

namespace ProjectR.Domain.Abstractions
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
    }
}
