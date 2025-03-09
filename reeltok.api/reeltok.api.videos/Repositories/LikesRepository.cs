using reeltok.api.videos.Data;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace reeltok.api.videos.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly VideosDbContext _context;
        public LikesRepository(VideosDbContext context)
        {
            _context = context;
        }

        public async Task<List<TotalVideoLikesEntity>> GetTotalLikesForVideosAsync(List<Guid> videoIds)
        {
            List<TotalVideoLikesEntity> totalLikes = await _context.VideosLikes
                .Where(v => videoIds.Contains(v.VideoId))
                .Select(v => new TotalVideoLikesEntity(v.VideoId, v.TotalLikes))
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return totalLikes;
        }

        // Increment TotalLikes
        public async Task IncrementTotalLikesAsync(Guid videoId)
        {
            VideoTotalLikesEntity? videoLikeEntity = await _context.VideosLikes
                .FirstOrDefaultAsync(vl => vl.VideoId == videoId).ConfigureAwait(false);

            if (videoLikeEntity != null)
            {
                videoLikeEntity.TotalLikes += 1;
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                // Handle the case where the VideoLikesEntity doesn't exist
                throw new Exception("Video likes entity not found.");
            }
        }

        // Decrement TotalLikes
        public async Task DecrementTotalLikesAsync(Guid videoId)
        {
            VideoTotalLikesEntity? videoLikeEntity = await _context.VideosLikes
                .FirstOrDefaultAsync(vl => vl.VideoId == videoId).ConfigureAwait(false);

            if (videoLikeEntity != null)
            {
                if (videoLikeEntity.TotalLikes > 0)
                {
                    videoLikeEntity.TotalLikes -= 1;
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    throw new Exception("Cannot decrement likes. Total likes are already 0.");
                }
            }
            else
            {
                throw new Exception("Video likes entity not found.");
            }
        }

    }
}
