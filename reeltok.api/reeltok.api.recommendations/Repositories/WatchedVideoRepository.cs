using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class WatchedVideoRepository : IWatchedVideoRepository
    {
        private readonly RecommendationDbContext _context;

        public WatchedVideoRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity)
        {
            await _context.WatchedVideoEntities.AddAsync(watchedVideoEntity).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }

        public async Task<WatchedVideoEntity?> GetByVideoAndUserAsync(Guid videoId, Guid userId)
        {
            return await _context.WatchedVideoEntities.FirstOrDefaultAsync(w => w.WatchedVideoDetails.VideoId == videoId && w.WatchedVideoDetails.UserId == userId).ConfigureAwait(false);
        }
        
        public async Task<bool> UpdateWatchedVideoAsync(WatchedVideoEntity watchedVideoEntity)
        {
            _context.WatchedVideoEntities.Update(watchedVideoEntity);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return true;
        }
    }
}
