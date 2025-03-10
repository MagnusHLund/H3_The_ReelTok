using reeltok.api.comments.Data;
using Microsoft.EntityFrameworkCore;
using reeltok.api.comments.Entities;
using reeltok.api.comments.Interfaces.Repositories;

namespace reeltok.api.comments.Repositories
{
    public class CommentRepository : ICommentsRepository
    {
        private readonly CommentsDbContext _context;

        public CommentRepository(CommentsDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalCommentsForVideoAsync(Guid videoId)
        {
            int comments = await _context.Comments
                .Where(c => c.CommentDetails.VideoId == videoId)
                .CountAsync()
                .ConfigureAwait(false);

            return comments;
        }

        public async Task<List<CommentEntity>> GetCommentsByVideoIdAsync(Guid videoId, int pageNumber, byte pageSize)
        {
            int skip = pageNumber * pageSize;

            List<CommentEntity> comments = await _context.Comments
                .Where(c => c.CommentDetails.VideoId == videoId)
                .Skip(skip)
                .Take(pageSize)
                .ToListAsync()
                .ConfigureAwait(false);

            return comments;
        }

        public async Task<CommentEntity> CreateCommentAsync(CommentEntity comment)
        {
            CommentEntity savedComment = (await _context.Comments.AddAsync(comment)
                .ConfigureAwait(false)).Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);
            return savedComment;
        }
    }
}
