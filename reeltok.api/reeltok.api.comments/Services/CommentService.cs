using reeltok.api.comments.Entities;
using reeltok.api.comments.Interface;

namespace reeltok.api.comments.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repo;

        public CommentService(ICommentRepository repository)
        {
            _repo = repository;
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            Comment comment1;

            try
            {
                comment1 = await _repo.CreateCommentAsync(comment).ConfigureAwait(false);
            }
            catch
            {
                throw new InvalidOperationException("Couldn't create comments. Please try again");
            }

            return comment1;
        }

        public async Task<List<Comment>> GetAllCommentByVideoId(Guid videoId)
        {
            List<Comment> comments;

            try
            {
                comments = await _repo.GetAllCommentByVideoId(videoId).ConfigureAwait(false);
            }
            catch
            {
                throw new InvalidOperationException("Couldn't fetch comments. Please try again");
            }

            return comments;
        }
    }
}
