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

    public string Description { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public ICollection<Thread> Threads { get; } = new List<Thread>();
}
