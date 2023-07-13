namespace ProjectR.Domain.Abstractions;

public interface IThreadRepository
{
    void InsertThread(Entities.Thread thread);

    Task<Entities.Thread?> GetThreadByIdAsync(Guid threadId);


}
