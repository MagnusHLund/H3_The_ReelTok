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

        public async Task<uint> AddUserInterestAsync(CategoryUserInterestEntity categoryUserInterestEntity)
        {
            UserEntity savedUserEntity = (await _context.UserInterests.AddAsync(categoryUserInterestEntity.UserInterest)
                .ConfigureAwait(false)).Entity;

            CategoryUserInterestEntity savedCategoryUserInterestEntity = (await _context.CategoryUserInterests
                .AddAsync(categoryUserInterestEntity)
                .ConfigureAwait(false)).Entity;

            await _context.SaveChangesAsync().ConfigureAwait(false);

            if (savedUserEntity.UserInterestId != savedCategoryUserInterestEntity.UserInterest.UserInterestId)
            {
                throw new InvalidOperationException
                    ("The saved user entity does not match the user interest in the savedCategoryUserInterestEntity.");
            }

            return savedCategoryUserInterestEntity.CategoryId;
        }


        public async Task<uint> UpdateUserInterestAsync(UserEntity user, uint newCategoryId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                await RemoveExistingUserInterestAsync(user.UserId).ConfigureAwait(false);

                CategoryUserInterestEntity newUserInterest = new CategoryUserInterestEntity(user, newCategoryId);

                await _context.CategoryUserInterests.AddAsync(newUserInterest).ConfigureAwait(false);

                await _context.SaveChangesAsync().ConfigureAwait(false);
                await transaction.CommitAsync().ConfigureAwait(false);

                return newUserInterest.CategoryId;
            }
            catch
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public async Task RemoveExistingUserInterestAsync(Guid userId)
        {
            UserEntity userInterest = await _context.UserInterests
                .Where(ui => ui.UserId == userId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false)
                ?? throw new KeyNotFoundException($"No interest found for user with id: {userId}");

            _context.UserInterests.Remove(userInterest);

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
