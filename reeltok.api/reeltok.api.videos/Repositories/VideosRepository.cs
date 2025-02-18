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

        public Task<VideoEntity> UpdateVideoStreamUrl(Guid videoId, string streamUrl)
        {
            throw new NotImplementedException();
        }

        public Task<string> DeleteVideoAsync(Guid userId, Guid videosId)
        {
            throw new NotImplementedException();
        }

        public Task<List<VideoEntity>> GetVideosForFeedAsync(List<Guid> videoIds)
        {
            throw new NotImplementedException();
        }

        public Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
