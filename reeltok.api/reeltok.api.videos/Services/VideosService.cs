using reeltok.api.videos.Entities;
using reeltok.api.videos.Interfaces;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Services
{
    public class VideosService : IVideosService
    {
        public Task<bool> DeleteVideo(Guid userId, Guid videosId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Video>> GetVideosForFeed(Guid userId, byte amount)
        {
            throw new NotImplementedException();
        }

        public Task<List<Video>> GetVideosForProfile(Guid userId, byte amountToReturn, uint amountReceived)
        {
            throw new NotImplementedException();
        }

        public Task<Video> UploadVideo(VideoUpload video)
        {
            throw new NotImplementedException();
        }
    }
}
