using ProjectR.Domain.Primitives;

namespace ProjectR.Domain.Entities;
public sealed class Epic : Entity
{
    public Epic(Guid id, string name)
        : base(id)
    {
        Name = name;
    }

    public string Name { get; set; }

    public string Description { get; set; } = String.Empty;

    public ICollection<User> Users { get; set; } = new HashSet<User>();

    public ICollection<Thread> Threads { get; set; } = new HashSet<Thread>();
}
