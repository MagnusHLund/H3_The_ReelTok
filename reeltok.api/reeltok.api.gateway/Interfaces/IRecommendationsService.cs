using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.Interfaces
{
    public interface IRecommendationsService
    {
        public Task<List<RecommendedCategories>> GetRecommendation(Guid userId);
        public Task<bool> UpdateRecommendation(Recommendations recommendationCategory);
    }
}