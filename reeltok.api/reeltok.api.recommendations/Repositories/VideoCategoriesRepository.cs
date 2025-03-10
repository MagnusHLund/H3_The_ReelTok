using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class VideoCategoriesRepository : IVideoCategoriesRepository
    {
        private readonly RecommendationDbContext _context;

        public VideoCategoriesRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> AddVideoCategoryAsync(CategoryVideoCategoryEntity categoryVideoCategoryEntity)
        {
            VideoEntity savedVideoEntity = (await _context.VideoCategories
                .AddAsync(categoryVideoCategoryEntity.VideoCategory)
                .ConfigureAwait(false)).Entity;

            CategoryVideoCategoryEntity savedCategoryVideoCategoryEntity = (await _context.CategoryVideoCategories
                .AddAsync(categoryVideoCategoryEntity)
                .ConfigureAwait(false)).Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            if (savedVideoEntity.VideoCategoryId != savedCategoryVideoCategoryEntity.VideoCategoryId)
            {
                throw new InvalidOperationException
                    ("The saved video entity does not match the video category in the savedCategoryVideoCategoryEntity.");
            }

            return savedCategoryVideoCategoryEntity.CategoryId;
        }
    }
}