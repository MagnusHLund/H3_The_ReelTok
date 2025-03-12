using reeltok.api.gateway.Entities.comments;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface ICommentsService
    {
        Task<CommentUsingDateTime> AddComment(Guid videoId, string message);
        Task<List<CommentUsingDateTime>> LoadComments(Guid videoId, byte amount);
    }
}
