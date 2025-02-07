
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces
{
    public interface IRecommendationsRepository
    {
        Task<List<RecommendedCategories>> GetRecommendationAsync(Guid userId);
        Task UpdateRecommendationAsync(Recommendations recommendations);
    }
}
