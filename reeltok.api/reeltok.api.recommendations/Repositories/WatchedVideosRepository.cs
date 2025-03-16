using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Utils;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class WatchedVideosRepository : IWatchedVideosRepository
    {
        private readonly RecommendationDbContext _context;

        public WatchedVideosRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public async Task<List<WatchedVideoEntity>> GetExistingWatchedVideosAsync(Guid userId, List<Guid> videoIds)
        {
            List<WatchedVideoEntity> watchedVideos = await _context.WatchedVideos
                .Where(wv => wv.UserId == userId && videoIds.Contains(wv.VideoId))
                .ToListAsync()
                .ConfigureAwait(false);

            return watchedVideos;
        }

        public async Task AddNewWatchedVideosAsync(List<WatchedVideoEntity> newWatchedVideos)
        {
            await _context.WatchedVideos.AddRangeAsync(newWatchedVideos).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task UpdateWatchedVideosAsync(List<WatchedVideoEntity> watchedVideos)
        {
            long currentUtcTime = DateTimeUtils.DateTimeToUnixTime(DateTime.UtcNow);
            foreach (WatchedVideoEntity watchedVideo in watchedVideos)
            {
                watchedVideo.WatchCount++;
                watchedVideo.LastWatchedAt = currentUtcTime;
            }
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}