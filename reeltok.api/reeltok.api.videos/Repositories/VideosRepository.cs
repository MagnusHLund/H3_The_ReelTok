using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Repositories
{
    public class VideosRepository : IVideosRepository
    {
        public Task<VideoEntity> CreateVideoAsync(VideoEntity video)
        {
            throw new NotImplementedException();
        }

        public Task<VideoEntity> CreateVideoAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteVideoAsync(Guid userId, Guid videosId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VideoEntity>> GetVideosForFeedAsync(Guid userId, byte amount)
        {
            throw new NotImplementedException();
        }
    }
}
