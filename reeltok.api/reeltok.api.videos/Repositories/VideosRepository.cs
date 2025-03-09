using Microsoft.EntityFrameworkCore;
using reeltok.api.videos.Data;
using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Repositories
{
    public class VideosRepository : IVideosRepository
    {
        private readonly VideosDbContext _context;

        public VideosRepository(VideosDbContext context)
        {
            _context = context;
        }

        // TODO: Add repository logic

        public async Task<VideoEntity> CreateVideoAsync(VideoEntity video)
        {
            VideoEntity videoEntity = (await _context.Videos.AddAsync(video).ConfigureAwait(false)).Entity;
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return videoEntity;
        }

        public async Task<VideoEntity> UpdateVideoStreamPathAsync(Guid videoId, string streamPath)
        {
            VideoEntity video = await _context.Videos.FindAsync(videoId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Video not found!");

            video.StreamPath = streamPath;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return video;
        }

        public async Task DeleteVideoAsync(Guid userId, Guid videosId)
        {
            VideoEntity video = await _context.Videos.FirstOrDefaultAsync(v => v.VideoId == videosId && v.UserId == userId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException("Video not found or unauthorized!");

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<List<VideoEntity>> GetVideosForFeedAsync(List<Guid> videoIds, byte amount)
        {
            List<VideoEntity> videosForFeed = await _context.Videos
                .Where(v => videoIds.Contains(v.VideoId))
                .AsNoTracking()
                .Take(amount)
                .ToListAsync()
                .ConfigureAwait(false);

            return videosForFeed;
        }

        public async Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize)
        {
            List<VideoEntity> videoForProfile = await _context.Videos
                .Where(v => v.UserId == userId)
                .OrderByDescending(v => v.UploadedAt)
                .Skip((int) (pageNumber * pageSize))
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync()
                .ConfigureAwait(false);

            return videoForProfile;
        }
    }
}
