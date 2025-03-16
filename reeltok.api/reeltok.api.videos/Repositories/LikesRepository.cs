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

            if (videoLikeEntity == null)
            {
                await CreateNewLikesEntityAsync(videoId).ConfigureAwait(false);
                return;
            }

            videoLikeEntity.TotalLikes += 1;
            await _context.SaveChangesAsync().ConfigureAwait(false);

        }

        // Decrement TotalLikes
        public async Task DecrementTotalLikesAsync(Guid videoId)
        {
            VideoTotalLikesEntity? videoLikeEntity = await _context.VideosLikes
                .FirstOrDefaultAsync(vl => vl.VideoId == videoId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find likes for video id: {videoId}");

            if (videoLikeEntity.TotalLikes < 0)
            {
                throw new Exception("Cannot decrement likes. Total likes are already 0.");
            }

            videoLikeEntity.TotalLikes -= 1;
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        private async Task CreateNewLikesEntityAsync(Guid videoId)
        {
            uint totalLikes = 1;
            VideoTotalLikesEntity videoLikeEntity = new VideoTotalLikesEntity(videoId, totalLikes);
            await _context.VideosLikes.AddAsync(videoLikeEntity).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
