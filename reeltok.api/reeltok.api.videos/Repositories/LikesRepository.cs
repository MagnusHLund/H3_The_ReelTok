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
    }
}
