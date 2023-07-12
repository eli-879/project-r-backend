using Microsoft.EntityFrameworkCore;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;

namespace ProjectR.Infrastructure
{
    public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Epic> Epics { get; set; }

        public DbSet<Domain.Entities.Thread> Threads { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlServer();
         }*/


    }
}
