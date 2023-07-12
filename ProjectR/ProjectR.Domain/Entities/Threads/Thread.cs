using ProjectR.Domain.Primitives;

namespace ProjectR.Domain.Entities;

public sealed class Thread : Entity
{
    public Thread(Guid id, Guid userId, Guid epicId, string title, string content) : base(id)
    {
        UserId = userId;
        EpicId = epicId;
        Title = title;
        Content = content;
    }

    private Thread(Guid id) : base(id)
    {

    }

    public string Title { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public Guid EpicId { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; }

    public Epic Epic { get; set; }
}
