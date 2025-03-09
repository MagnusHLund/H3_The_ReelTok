using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Interfaces.Services
{
    public interface IExternalApiService
    {
        Task<List<Guid>> GetRecommendedVideoIdsAsync(Guid userId, byte amount);
        Task<List<VideoCreatorEntity>> GetVideoCreatorDetailsAsync(List<Guid> videoIds);
        Task<bool> LikeVideoAsync(Guid userId, Guid videoId);
        Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId);
        Task<List<HasUserLikedVideoEntity>> HasUserLikedVideosAsync(Guid userId, List<Guid> videoIds);
        Task<bool> AddVideoIdToRecommendationAPI(Guid videoId, byte category);
    }
}