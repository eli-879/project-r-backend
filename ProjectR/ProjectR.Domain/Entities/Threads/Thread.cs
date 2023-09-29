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

    public string Title { get; set; }

    public string Content { get; set; }


    public Guid UserId { get; set; }

    public User User { get; set; }

    public Guid EpicId { get; set; }
    public Epic Epic { get; set; }

    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }


}
