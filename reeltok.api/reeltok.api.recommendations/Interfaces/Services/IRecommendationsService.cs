using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces.Services
{
    // TODO: Right now most of them are not returning anything, we need to add the return types
    public interface IRecommendationsService
    {
        Task<RecommendedCategories> GetAllCategoriesAsync();

        /*
        Task GetRecommendedVideosForUserAsync();
        */
    }
}
