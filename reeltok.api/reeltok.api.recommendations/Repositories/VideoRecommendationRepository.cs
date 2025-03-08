using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class VideoRecommendationRepository : IVideoRecommendationRepository
    {
        private readonly RecommendationDbContext _context;
        private readonly IRecommendationsRepository _recommendationsRepository;

        public VideoRecommendationRepository
            (RecommendationDbContext context, IRecommendationsRepository recommendationsRepository)
        {
            _recommendationsRepository = recommendationsRepository;
            _context = context;
        }

        public async Task<bool> AddRecommendationForVideoAsync(VideoCategoryEntity videoCategoryEntity, int categoryId)
        {
            CategoryEntity categoryEntity = await _recommendationsRepository
                .GetCategoryAsync((CategoryType)categoryId);

            if (categoryEntity == null)
            {
                return false;
            }

            videoCategoryEntity.Categories ??= new List<CategoryEntity>();

            videoCategoryEntity.Categories.Add(categoryEntity);

            await _context.VideoCategoryEntities.AddAsync(videoCategoryEntity).ConfigureAwait(false);

            await _context.SaveChangesAsync().ConfigureAwait(false);

            return true;
        }

        public async Task<VideoCategoryEntity?> GetVideoCategoryEntityAsync(Guid videoId)
        {
            return await _context.VideoCategoryEntities.Include(vc => vc.Categories).FirstOrDefaultAsync(vc => vc.VideoCategoryDetails.VideoId == videoId) ?? throw new KeyNotFoundException("Video not found");
        }
    }
}
