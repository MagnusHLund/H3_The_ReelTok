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
            throw new NotImplementedException();
        }

        public async Task DeleteVideoAsync(Guid userId, Guid videosId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VideoEntity>> GetVideosForFeedAsync(List<Guid> videoIds)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
