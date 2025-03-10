using reeltok.api.comments.Entities;

namespace reeltok.api.comments.Interfaces.Services
{
    public interface ICommentsService
    {
        Task<int> GetTotalCommentsForVideoAsync(Guid videoId);
        Task<List<CommentEntity>> GetCommentsByVideoIdAsync(Guid videoId, int pageNumber, byte pageSize);
        Task<CommentEntity> CreateCommentAsync(Guid videoId, Guid userId, string commentText);


    }
}
