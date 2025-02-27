using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces
{
    public interface IRecommendationsService
    {
        Task<RecommendedCategories> GetRecommendedCategoriesAsync();
        // TODO: Right now most of them are not returning anything, we need to add the return types
        Task AddRecommendationForUserAsync();
        Task AddRecommendationForVideoAsync();
        Task UpdateRecommendationForUserAsync();
    }
}
