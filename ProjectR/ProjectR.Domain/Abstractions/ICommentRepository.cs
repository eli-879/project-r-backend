using ProjectR.Domain.Entities;

namespace ProjectR.Domain.Abstractions;

public interface ICommentRepository
{
    Task InsertCommentAsync(Comment comment);

    Task<Comment?> GetCommentByIdAsync(Guid id);

    Task<IEnumerable<Comment>> GetCommentsFromThreadAsync(Guid threadId);
}
