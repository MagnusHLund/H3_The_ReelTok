using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.Interfaces
{
    public interface IRecommendationsService
    {
        Task<List<RecommendedCategories>> GetRecommendation(Guid userId);
        Task<bool> UpdateRecommendation(Recommendations recommendationCategory);
    }
}
