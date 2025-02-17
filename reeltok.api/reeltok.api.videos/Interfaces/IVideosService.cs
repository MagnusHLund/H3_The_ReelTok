using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Interfaces
{
    public interface IVideosService
    {
        Task<List<VideoEntity>> GetVideosForFeedAsync(Guid userId, byte amount);
        Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, byte amountToReturn, uint amountReceived);
        Task<VideoEntity> UploadVideoAsync(VideoUpload video, Guid userId);
        Task<bool> DeleteVideoAsync(Guid userId, Guid videosId);
    }
}
