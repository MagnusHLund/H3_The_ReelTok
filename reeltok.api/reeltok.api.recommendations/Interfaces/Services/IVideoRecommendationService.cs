using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IVideoRecommendationService
    {
        Task<bool> AddRecommendationForVideoAsync(VideoEntity videoCategory, int categoryId);
        Task<VideoEntity> GetVideoCategoryAsync(Guid videoId);
    }
}
