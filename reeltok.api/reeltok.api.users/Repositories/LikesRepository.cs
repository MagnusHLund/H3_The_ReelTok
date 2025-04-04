using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class LikesRepository : ILikesRepository
    {
        private readonly UserDbContext _context;

        public LikesRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToLikedVideoAsync(LikedVideoEntity likedVideo)
        {
            LikedVideoEntity likedVideoEntity = (await _context.LikedVideos.AddAsync(likedVideo).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return likedVideoEntity != null;
        }

        public async Task<bool> RemoveFromLikedVideoAsync(Guid userId, Guid videoId)
        {
            LikedVideoEntity likedVideoEntity = await _context.LikedVideos.FirstOrDefaultAsync(
                lv => lv.UserId == userId && lv.VideoId == videoId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"Unable to find liked video with user id {userId} and video id {videoId}!");

            _context.LikedVideos.Remove(likedVideoEntity);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<List<HasUserLikedVideoEntity>> CheckUserLikesForVideosAsync(Guid userId, List<Guid> videoIds)
        {
            List<HasUserLikedVideoEntity> likedVideos = await _context.LikedVideos
                .Where(lv => lv.UserId == userId && videoIds.Contains(lv.VideoId))
                .Select(lv => new HasUserLikedVideoEntity(lv.VideoId, true))
                .ToListAsync()
                .ConfigureAwait(false);

            return likedVideos;
        }
    }
}
