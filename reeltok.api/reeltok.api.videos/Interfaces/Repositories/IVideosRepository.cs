using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Interfaces
{
    public interface IVideosRepository
    {
        Task<List<VideoEntity>> GetVideosForFeedAsync(List<Guid> videoIds, byte amount);
        Task<VideoEntity> CreateVideoAsync(VideoEntity video);
        Task<string> DeleteVideoAsync(Guid userId, Guid videosId);
        Task<List<VideoEntity>> GetVideosForProfileAsync(Guid userId, uint pageNumber, byte pageSize);
        Task<List<Guid>> GetRandomVideoIdsAsync(Guid userId, byte amount);
        Task<VideoEntity> GetVideoByIdAsync(Guid videoId);
    }
}
