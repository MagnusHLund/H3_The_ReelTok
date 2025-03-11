using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Enums;

namespace reeltok.api.gateway.Interfaces.Services
{
    public interface IRecommendationsService
    {
        Task<List<CategoryType>> GetRecommendation(Guid userId);
        Task<bool> UpdateRecommendation(Recommendations recommendationCategory);
    }
}
