using reeltok.api.gateway.Entities;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Interfaces
{
    public interface IVideosService
    {
        Task<bool> LikeVideo(Guid VideoId);
        Task<bool> RemoveLikeFromVideo(Guid VideoId);
        Task<List<Video>> GetVideosForFeed(byte amount);
        Task<Video> UploadVideo(VideoUpload video);
        Task<bool> DeleteVideo(Guid videoId);
        Task<List<Video>> GetVideosForProfile(Guid userId, byte amountToReturn, uint amountReceived);

    }
}
