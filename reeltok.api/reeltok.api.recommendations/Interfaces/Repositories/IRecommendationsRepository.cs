using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IRecommendationsRepository
    {

        Task<CategoryEntity> GetCategoryAsync(RecommendedCategories category);


        /*
            Over here we will have the method that will be called when the algorithm is ready to make a recommendation.
        */
    }
}
