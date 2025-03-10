using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private readonly IRecommendationsRepository _recommendationsRepository;

        public RecommendationsService(IRecommendationsRepository recommendationsRepository)
        {
            _recommendationsRepository = recommendationsRepository;
        }

        public async Task<List<Guid>> GetVideoRecommendationsForUserAsync(Guid userId, byte amountOfVideos)
        {
            List<Guid> recommendedVideos = await _recommendationsRepository
                .GetRecommendedVideosByUserAsync(userId, amountOfVideos).ConfigureAwait(false);

            return recommendedVideos;
        }
    }
}
