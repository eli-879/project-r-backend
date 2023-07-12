using ProjectR.Domain.Primitives;

namespace ProjectR.Domain.Entities;

public sealed class Thread : Entity
{
    public Thread(Guid id, Guid userId, Guid epicId) : base(id)
    {
        UserId = userId;
        EpicId = epicId;
    }

    private Thread(Guid id) : base(id)
    {

    }

    string Name { get; set; } = string.Empty;

    Guid EpicId { get; set; }

    Guid UserId { get; set; }

    public User User { get; set; }

    public Epic Epic { get; set; }
}
