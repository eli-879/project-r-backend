using ProjectR.Domain.Primitives;

namespace ProjectR.Domain.Entities;
public sealed class Comment : Entity
{
    public Comment(Guid id, string message) : base(id)
    {
        Message = message;
        CreatedAt = DateTime.Now;


    }

    public Guid? UserId { get; set; }

    public User? User { get; set; }

    public Guid ThreadId { get; set; }

    public Thread Thread { get; set; }

    public string Message { get; set; } = string.Empty;

    public ICollection<Comment> ChildComments { get; set; } = new List<Comment>();

    public Guid? CommentId { get; set; }

    public DateTime CreatedAt { get; set; }

    public void AddChildComment(Comment comment)
    {
        ChildComments.Add(comment);
    }



}
