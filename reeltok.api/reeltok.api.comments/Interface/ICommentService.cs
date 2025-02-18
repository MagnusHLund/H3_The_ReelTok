using reeltok.api.comments.Entities;

namespace reeltok.api.comments.Interface
{
    public interface ICommentService
    {
        Task<Comment> CreateCommentAsync(Comment comment);
        Task<List<Comment>> GetAllCommentByVideoId(Guid videoId);

    }
}
