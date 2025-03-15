using reeltok.api.videos.Entities;

namespace reeltok.api.videos.Interfaces.Services
{
    public interface IExternalApiService
    {
        Task<List<Guid>> GetRecommendedVideoIdsAsync(Guid userId, byte amount);
        Task<List<UserEntity>> GetVideoCreatorDetailsAsync(List<Guid> userIds);
        Task<bool> LikeVideoAsync(Guid userId, Guid videoId);
        Task<bool> RemoveLikeFromVideoAsync(Guid userId, Guid videoId);
        Task<List<HasUserLikedVideoEntity>> HasUserLikedVideosAsync(Guid userId, List<Guid> videoIds);
        Task<bool> AddVideoToRecommendationsApiAsync(Guid videoId, byte category);
        Task<bool> DeleteVideoFromRecommendationsApiAsync(Guid videoId);
    }
}