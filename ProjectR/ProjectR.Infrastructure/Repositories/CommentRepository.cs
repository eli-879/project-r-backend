using Microsoft.EntityFrameworkCore;
using ProjectR.Domain.Abstractions;
using ProjectR.Domain.Entities;

namespace ProjectR.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comment?> GetCommentByIdAsync(Guid id)
    {
        var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.Id == id);
        return comment;

    }

    public async Task<IEnumerable<Comment>> GetCommentsFromThreadAsync(Guid threadId)
    {
        IEnumerable<Comment> comments = await _dbContext.Comments
                                                        .Include(c => c.User)
                                                        .Where(c => c.ThreadId == threadId)
                                                        .ToListAsync();

        return comments;
    }

    public async Task InsertCommentAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
    }
}
