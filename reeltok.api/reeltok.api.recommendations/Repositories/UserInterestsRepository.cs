using Microsoft.EntityFrameworkCore;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class UserInterestsRepository : IUserInterestsRepository
    {
        private readonly RecommendationDbContext _context;

        public UserInterestsRepository(RecommendationDbContext context)
        {
            _context = context;
        }

        public async Task<CategoryEntity> GetUserInterestAsync(Guid userId)
        {
            CategoryEntity interests = await _context.CategoryUserInterests
                .Where(cui => cui.UserInterest.UserId == userId)
                .Select(cui => cui.Category)
                .FirstOrDefaultAsync()
                ?? throw new KeyNotFoundException($"No interest found for user with id: {userId}");

            return interests;
        }

        public async Task<CategoryUserInterestEntity> AddUserInterestAsync(CategoryUserInterestEntity categoryUserInterestEntity)
        {
            UserEntity savedUserEntity = (await _context.UserInterests.AddAsync(categoryUserInterestEntity.UserInterest)).Entity;
            CategoryUserInterestEntity savedCategoryUserInterestEntity = (await _context.CategoryUserInterests
                .AddAsync(categoryUserInterestEntity)).Entity;

            await _context.SaveChangesAsync();

            if (savedUserEntity != savedCategoryUserInterestEntity.UserInterest)
            {
                throw new InvalidOperationException
                    ("The saved user entity does not match the user interest in the saved category user interest entity.");
            }

            return savedCategoryUserInterestEntity;
        }

        public async Task<bool> UpdateUserInterestAsync(Guid userId, uint categoryId)
        {
            var categoryUserInterest = await _context.CategoryUserInterests
                .Include(cui => cui.UserInterest)
                .FirstOrDefaultAsync(
                    cui => cui.UserInterest.UserId == categoryUserInterestEntity.UserInterest.UserId &&
                    cui.CategoryId == oldCategoryId)
                ?? throw KeyNotFoundException("");

            categoryUserInterest.CategoryId = newCategoryId;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}