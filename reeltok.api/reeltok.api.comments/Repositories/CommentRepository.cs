using Microsoft.EntityFrameworkCore;
using reeltok.api.comments.Data;
using reeltok.api.comments.Entities;
using reeltok.api.comments.Interface;

namespace reeltok.api.comments.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommentDbContext _context;

        public CommentRepository(CommentDbContext context)
        {
            _context = context;
        }
        public async Task<CommentEntity> CreateCommentAsync(CommentEntity comment)
        {
            CommentEntity comment1 = (await _context.Comments.AddAsync(comment).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return comment1;
        }

        public async Task<List<CommentEntity>> GetAllCommentByVideoId(Guid videoId)
        {
            return await _context.Comments.Where(c => c.CommentDetails.VideoId == videoId).ToListAsync().ConfigureAwait(false);
        }
    }
}
