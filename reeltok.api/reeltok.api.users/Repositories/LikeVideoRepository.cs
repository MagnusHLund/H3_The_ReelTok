using Microsoft.EntityFrameworkCore;
using reeltok.api.users.Data;
using reeltok.api.users.Entities;
using reeltok.api.users.Interfaces.Repositories;

namespace reeltok.api.users.Repositories
{
    public class LikeVideoRepository : ILikeVideoRepository
    {
        private readonly UserDBContext _context;

        public LikeVideoRepository(UserDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToLikedVideoAsync(LikedVideo likedVideo)
        {
            LikedVideo? DblikedVideo = (await _context.LikedVideos.AddAsync(likedVideo).ConfigureAwait(false)).Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return DblikedVideo != null;
        }

        public async Task<bool> RemoveFromLikedVideoAsync(Guid userId, Guid videoId)
        {
            LikedVideo? likedVideo = await _context.LikedVideos.FirstOrDefaultAsync(lv => lv.LikedVideoDetails.UserId == userId && lv.LikedVideoDetails.VideoId == videoId).ConfigureAwait(false);

            if (likedVideo == null)
            {
                return false;
            }

            _context.LikedVideos.Remove(likedVideo);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }
    }
}