using reeltok.api.comments.Entities;

namespace reeltok.api.comments.Interfaces.Repositories
{
    public interface ICommentsRepository
    {
        Task<int> GetTotalCommentsForVideoAsync(Guid videoId);
        Task<List<CommentEntity>> GetCommentsByVideoIdAsync(Guid videoId, int pageNumber, byte pageSize);
        Task<CommentEntity> CreateCommentAsync(CommentEntity comment);
    }
}
