using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
