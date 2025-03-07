using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IVideoRecommendationService
    {
        Task<bool> AddRecommendationForVideoAsync(VideoCategoryEntity videoCategory, int categoryId);
        Task<VideoCategoryEntity> GetVideoCategoryAsync(Guid videoId);
    }
}
