using reeltok.api.comments.Entities;
using reeltok.api.comments.Factories;
using reeltok.api.comments.Interfaces.Services;
using reeltok.api.comments.Interfaces.Repositories;

namespace reeltok.api.comments.Services
{
    public class CommentService : ICommentsService
    {
        private readonly ICommentsRepository _commentsRepository;
        private readonly IExternalApiService _externalApisService;

        public CommentService(ICommentsRepository commentsRepository, IExternalApiService externalApisService)
        {
            _commentsRepository = commentsRepository;
            _externalApisService = externalApisService;
        }

        public async Task<int> GetTotalCommentsForVideoAsync(Guid videoId)
        {
            int totalVideoComments = await _commentsRepository.GetTotalCommentsForVideoAsync(videoId)
                .ConfigureAwait(false);

            return totalVideoComments;
        }

        public async Task<List<CommentEntity>> GetCommentsByVideoIdAsync(Guid videoId, int pageNumber, byte pageSize)
        {
            List<CommentEntity> comments = await _commentsRepository.GetCommentsByVideoIdAsync(videoId, pageNumber, pageSize)
                .ConfigureAwait(false);

            return comments;
        }

        public async Task<CommentEntity> CreateCommentAsync(Guid videoId, Guid userId, string commentText)
        {
            await _externalApisService.EnsureVideoIdExistAsync(videoId).ConfigureAwait(false);

            CommentEntity commentEntity = CommentFactory.CreateCommentEntity(videoId, userId, commentText);
            CommentEntity savedCommentEntity = await _commentsRepository.CreateCommentAsync(commentEntity).ConfigureAwait(false);

            return savedCommentEntity;
        }
    }
}
