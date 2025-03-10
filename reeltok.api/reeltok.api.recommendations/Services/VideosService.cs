using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Factories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Mappers;

namespace reeltok.api.recommendations.Services
{
    public class VideosService : IVideosService
    {
        private readonly IVideoCategoriesRepository _videoCategoriesRepository;
        private readonly IRecommendationsService _recommendationsService;

        public VideosService(IVideoCategoriesRepository videoCategoriesRepository, IRecommendationsService recommendationsService)
        {
            _videoCategoriesRepository = videoCategoriesRepository;
            _recommendationsService = recommendationsService;
        }

        public async Task<List<Guid>> GetRecommendedVideosForUsersFeedAsync(Guid userId, byte amountOfVideos)
        {
            byte maxAmount = 20;

            if (amountOfVideos > maxAmount)
            {
                amountOfVideos = maxAmount;
            }

            List<Guid> videoIds = await _recommendationsService
                .GetVideoRecommendationsForUserAsync(userId, amountOfVideos)
                .ConfigureAwait(false);

            return videoIds;
        }

        public async Task<CategoryType> AddVideoCategoryAsync(Guid videoId, CategoryType videoCategory)
        {
            CategoryEntity categoryEntityToSave = CategoryFactory.CreateCategoryEntity(videoCategory);
            VideoEntity videoEntity = new VideoEntity(videoId);

            CategoryVideoCategoryEntity categoryVideoCategoryEntity = CategoryFactory
                .CreateCategoryVideoInterestEntity(categoryEntityToSave, videoEntity);

            uint savedCategoryId = await _videoCategoriesRepository
                .AddVideoCategoryAsync(categoryVideoCategoryEntity)
                .ConfigureAwait(false);

            CategoryType savedVideoCategory = CategoryMapper.ConvertCategoryIdToCategoryType(savedCategoryId);

            return savedVideoCategory;
        }
    }
}
