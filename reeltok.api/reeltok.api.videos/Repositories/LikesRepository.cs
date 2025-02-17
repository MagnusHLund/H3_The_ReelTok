using Microsoft.EntityFrameworkCore;
using reeltok.api.videos.Data;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly VideosDbContext _context;
        public LikesRepository(VideosDbContext context)
        {
            _context = context;
        }

        public async Task<uint> GetTotalVideoLikesAsync(Guid videoId)
        {
            VideoLikesEntity video = await _context.VideosLikes
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VideoId == videoId)
                .ConfigureAwait(false)
                ?? throw new InvalidOperationException("Unable to find video!");

            return video.TotalLikes;
        }
    }
}
