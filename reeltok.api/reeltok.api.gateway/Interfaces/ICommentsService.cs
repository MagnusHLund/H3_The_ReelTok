using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface ICommentsService
    {
        public Task<CommentUsingDateTime> AddComment(Guid videoId, string commentText);
        public Task<List<CommentUsingDateTime>> LoadComments(Guid videoId, byte amount);
    }
}