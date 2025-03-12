using reeltok.api.gateway.Entities.comments;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface ICommentsService
    {
        Task<CommentUsingDateTime> AddCommentAsync(Guid videoId, string message);
        Task<List<CommentUsingDateTime>> LoadCommentsAsync(Guid videoId, int pageNumber, byte pageSize);
    }
}
