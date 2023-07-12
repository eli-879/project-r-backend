using ProjectR.Domain.Primitives;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectR.Domain.Entities;

public sealed class User : Entity
{
    public User(Guid id, string username, string email, string password, DateTime createdAt)
        : base(id)
    {
        Username = username;
        Password = password;
        Email = email;
        CreatedAt = createdAt;
    }

    [Index(IsUnique = true)]
    public string Username { get; private set; } = string.Empty;

    public string Password { get; private set; }

    public string Email { get; private set; } = string.Empty;

    public DateTime CreatedAt { get; private set; }

    public ICollection<Thread> Threads { get; private set; } = new HashSet<Thread>();
    public ICollection<Comment> Comments { get; private set; } = new HashSet<Comment>();
    public ICollection<Epic> Epics { get; private set; } = new HashSet<Epic>();
}
