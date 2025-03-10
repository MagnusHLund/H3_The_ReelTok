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

        public async Task<CommentEntity> CreateCommentAsync(CommentEntity comment)
        {
            CommentEntity comment1;

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

        public async Task<List<CommentEntity>> GetAllCommentByVideoId(Guid videoId)
        {
            List<CommentEntity> comments;

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
