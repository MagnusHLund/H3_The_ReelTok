using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface ICommentsService
    {
        Task<CommentUsingDateTime> AddComment(Guid videoId, string commentText);
        Task<List<CommentUsingDateTime>> LoadComments(Guid videoId, byte amount);
    }
}
