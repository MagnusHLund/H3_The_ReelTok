using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Interfaces
{
    public interface IVideosService
    {
        public Task<bool> LikeVideo(Guid VideoId);
        public Task<bool> RemoveLikeFromVideo(Guid VideoId);
        public Task<List<Video>> GetVideosForFeed(byte amount);
        public Task<Video> UploadVideo(VideoUpload video);
        public Task<bool> DeleteVideo(Guid videoId);
        Task<List<Video>> GetVideosForProfile(Guid userId, byte amountToReturn, uint amountReceived);

    }
}