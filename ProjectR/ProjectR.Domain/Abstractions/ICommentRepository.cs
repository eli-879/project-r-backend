using ProjectR.Domain.Entities;

namespace ProjectR.Domain.Abstractions;

public interface ICommentRepository
{
    Task InsertCommentAsync(Comment comment);

    Task<Comment?> GetCommentByIdAsync(Guid id);

    Task<ICollection<Comment>> GetCommentsFromThreadAsync(Guid threadId);
}
