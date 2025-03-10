using reeltok.api.comments.Entities;

namespace reeltok.api.comments.Interface
{
    public interface ICommentRepository
    {
        Task<CommentEntity> CreateCommentAsync(CommentEntity comment);
        Task<List<CommentEntity>> GetAllCommentByVideoId(Guid videoId);
    }
}
