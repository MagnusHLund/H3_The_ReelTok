using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Mappers;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Factories;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserInterestsRepository _userInterestsRepository;

        public UsersService(IUserInterestsRepository userRecommendationRepository)
        {
            _userInterestsRepository = userRecommendationRepository;
        }

        public async Task<CategoryType> GetUserInterestAsync(Guid userId)
        {
            CategoryEntity categoryEntity = await _userInterestsRepository.GetUserInterestAsync(userId).ConfigureAwait(false);
            CategoryType userInterest = categoryEntity.CategoryType;

            return userInterest;
        }

        public async Task<CategoryType> AddInterestForUserAsync(Guid userId, CategoryType userInterest)
        {
            CategoryEntity categoryEntityToSave = CategoryFactory.CreateCategoryEntity(userInterest);
            UserEntity userEntity = new UserEntity(userId);

            CategoryUserInterestEntity categoryUserInterestEntity = CategoryFactory
                .CreateCategoryUserInterestEntity(categoryEntityToSave, userEntity);

            CategoryUserInterestEntity savedCategoryUserInterestEntity = await _userInterestsRepository
                .AddUserInterestAsync(categoryUserInterestEntity)
                .ConfigureAwait(false);

            CategoryType savedUserInterest = savedCategoryUserInterestEntity.Category.CategoryType;

            return savedUserInterest;
        }

        public async Task<CategoryType> UpdateInterestForUserAsync(Guid userId, CategoryType newUserInterest)
        {
            uint categoryId = CategoryMapper.ConvertCategoryTypeToCategoryId(newUserInterest);
            UserEntity userEntity = new UserEntity(userId);

            CategoryUserInterestEntity savedCategoryUserInterestEntity = await _userInterestsRepository
                .UpdateUserInterestAsync(userEntity, categoryId)
                .ConfigureAwait(false);

            CategoryType savedUserInterest = savedCategoryUserInterestEntity.Category.CategoryType;

            return savedUserInterest;
        }
    }
}
