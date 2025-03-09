using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Services
{
    public class VideosService : IVideoRecommendationService
    {
        private readonly IVideoRecommendationRepository _videoRecommendationRepository;

        public VideosService(IVideoRecommendationRepository videoRecommendationRepository)
        {
            _videoRecommendationRepository = videoRecommendationRepository;
        }

        public async Task<bool> AddRecommendationForVideoAsync(VideoEntity videoCategory, int categoryId)
        {
            bool isAdded = await _videoRecommendationRepository.AddRecommendationForVideoAsync(videoCategory, categoryId);

            if (!isAdded)
            {
                throw new Exception("Category not found");
            }

            return isAdded;
        }

        public async Task<VideoEntity> GetVideoCategoryAsync(Guid videoId)
        {
            VideoEntity videoCategoryEntity = await _videoRecommendationRepository.GetVideoCategoryEntityAsync(videoId);

            return videoCategoryEntity;
        }
    }
}
