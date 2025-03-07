using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Repositories;

namespace reeltok.api.recommendations.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private readonly IVideoRecommendationAlgorithmRepository _videoRecAlgRepo;

        public RecommendationsService(IVideoRecommendationAlgorithmRepository videoRecommendationAlgorithmRepository)
        {
            _videoRecAlgRepo = videoRecommendationAlgorithmRepository;
        }

        public Task<List<string>> GetAllCategoriesAsync()
        {
            List<string> categories = Enum.GetNames(typeof(RecommendedCategories)).ToList();
            return Task.FromResult(categories);
        }

        public async Task<List<Guid>> GetTopVideoByUserInterestAsync(Guid userId, int amount)
        {
            return await _videoRecAlgRepo.GetTopVideoByUserInterestAsync(userId, amount);
        }
    }
}
