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

        // TODO: This return type is kinda bad. Should include video id as well, for easier management
        public async Task<List<TotalVideoLikesEntity>> GetTotalLikesForVideosAsync(List<Guid> videoIds)
        { /*
            VideoTotalLikesEntity video = await _context.VideosLikes
                .AsNoTracking()
                .FirstOrDefaultAsync(v => v.VideoId == videoIds)
                .ConfigureAwait(false)
                ?? throw new InvalidOperationException("Unable to find video!");

            return video.TotalLikes; */

            throw new NotImplementedException("");
        }
    }
}
