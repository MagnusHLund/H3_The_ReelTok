using Microsoft.EntityFrameworkCore;
using reeltok.api.videos.Data;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly VideoDbContext _context;
        public LikesRepository(VideoDbContext context)
        {
            _context = context;
        }

        public async Task<uint> GetTotalVideoLikesAsync(Guid videoId) {
            Video? video = await _context.Videos
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VideoId == videoId)
                .ConfigureAwait(false)
                ?? throw new InvalidOperationException("Unable to find video!");

            return video.Likes;
        }
    }
}
