using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IVideoRecommendationRepository
    {
        Task<bool> AddRecommendationForVideoAsync(VideoCategoryEntity videoCategoryEntity, int categoryId);

        Task<VideoCategoryEntity> GetVideoCategoryEntityAsync(Guid videoId);
    }
}
