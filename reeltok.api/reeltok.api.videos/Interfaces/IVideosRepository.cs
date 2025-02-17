using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Interfaces
{
    public interface IVideosRepository
    {
        Task<List<VideoEntity>> GetVideosForFeedAsync(Guid userId, byte amount);
        Task<VideoEntity> CreateVideoAsync(VideoEntity video);
        Task<bool> DeleteVideoAsync(Guid userId, Guid videosId);
        Task<VideoEntity> CreateVideoAsync();
    }
}
