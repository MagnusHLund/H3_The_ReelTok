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
                .ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"No interest found for user with id: {userId}");

            return interests;
        }

        public async Task<CategoryUserInterestEntity> AddUserInterestAsync(CategoryUserInterestEntity categoryUserInterestEntity)
        {
            UserEntity savedUserEntity = (await _context.UserInterests.AddAsync(categoryUserInterestEntity.UserInterest)
                .ConfigureAwait(false)).Entity;

            CategoryUserInterestEntity savedCategoryUserInterestEntity = (await _context.CategoryUserInterests
                .AddAsync(categoryUserInterestEntity)
                .ConfigureAwait(false)).Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            if (savedUserEntity != savedCategoryUserInterestEntity.UserInterest)
            {
                throw new InvalidOperationException
                    ("The saved user entity does not match the user interest in the savedCategoryUserInterestEntity.");
            }

            return savedCategoryUserInterestEntity;
        }

        public async Task<CategoryUserInterestEntity> UpdateUserInterestAsync(UserEntity user, uint newCategoryId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                await RemoveExistingUserInterest(user.UserId).ConfigureAwait(false);

                CategoryEntity newCategory = await _context.Categories
                    .FirstOrDefaultAsync(c => c.CategoryId == newCategoryId)
                    .ConfigureAwait(false)
                    ?? throw new KeyNotFoundException($"Category not found: {newCategoryId}");

                CategoryUserInterestEntity newUserInterest = new CategoryUserInterestEntity(user, newCategory);

                await _context.CategoryUserInterests.AddAsync(newUserInterest).ConfigureAwait(false);

                await _context.SaveChangesAsync().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return newUserInterest;
            }
            catch
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task RemoveExistingUserInterest(Guid userId)
        {
            CategoryUserInterestEntity userInterest = await _context.CategoryUserInterests
                .Include(cui => cui.UserInterest)
                .FirstOrDefaultAsync(cui => cui.UserInterest.UserId == userId).ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"No interest found for user with id: {userId}");

            _context.CategoryUserInterests.Remove(userInterest);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}