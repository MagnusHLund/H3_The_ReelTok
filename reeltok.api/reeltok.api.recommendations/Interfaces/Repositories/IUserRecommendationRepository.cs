using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IUserRecommendationRepository
    {
        Task<UserInterestEntity> GetUserInterestAsync(Guid userId);
        Task<bool> AddRecommendationForUserAsync(UserInterestEntity userInterest, int categoryId);
        Task<bool> UpdateRecommendationForUserAsync(UserInterestEntity userInterest, int oldCategoryId, int newCategoryId);
    }
}
