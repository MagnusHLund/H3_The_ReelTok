using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces.Services;

namespace reeltok.api.recommendations.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        public Task<List<string>> GetAllCategoriesAsync()
        {
            List<string> categories = Enum.GetNames(typeof(RecommendedCategories)).ToList();
            return Task.FromResult(categories);
        }
    }
}
