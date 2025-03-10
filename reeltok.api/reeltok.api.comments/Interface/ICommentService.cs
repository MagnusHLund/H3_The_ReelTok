using reeltok.api.comments.Entities;

namespace reeltok.api.comments.Interface
{
    public interface ICommentService
    {
        Task<CommentEntity> CreateCommentAsync(CommentEntity comment);
        Task<List<CommentEntity>> GetAllCommentByVideoId(Guid videoId);

    }
}
