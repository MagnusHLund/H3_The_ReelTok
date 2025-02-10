
using reeltok.api.recommendations.DTOs;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces
{
    public interface IRecommendationsService
    {
        public Task<List<RecommendedCategories>> GetRecommendation(Guid userId);
        public Task<bool> UpdateRecommendation(Recommendations recommendation);


    }
}