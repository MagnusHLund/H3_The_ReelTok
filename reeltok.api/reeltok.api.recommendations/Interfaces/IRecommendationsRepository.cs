
using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces
{
    public interface IRecommendationsRepository
    {
        Task<Recommendations> GetRecommendationAsync(Guid userId);
        Task UpdateRecommendationAsync(Recommendations recommendation, Guid userId);
    }
}
