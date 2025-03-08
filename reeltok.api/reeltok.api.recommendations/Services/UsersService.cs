using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Services;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUserRecommendationRepository _userRecommendationRepository;

        public UsersService(IUserRecommendationRepository userRecommendationRepository)
        {
            _userRecommendationRepository = userRecommendationRepository;
        }

        public async Task<bool> AddRecommendationForUserAsync(UserInterestEntity userInterest, int categoryId)
        {
            bool isAdded = await _userRecommendationRepository.AddRecommendationForUserAsync(userInterest, categoryId);

            if (!isAdded)
            {
                throw new Exception("Category not found");
            }

            return isAdded;
        }

        public async Task<UserInterestEntity?> GetUserInterestAsync(Guid userId)
        {
            UserInterestEntity userInterestEntity = await _userRecommendationRepository.GetUserInterestAsync(userId);

            return userInterestEntity;
        }

        public async Task<bool> UpdateRecommendationForUserAsync(Guid userId, int oldCategoryId, int newCategoryId)
        {
            UserInterestEntity userInterestEntity = await _userRecommendationRepository.GetUserInterestAsync(userId);

            bool isUpdated = await _userRecommendationRepository.UpdateRecommendationForUserAsync(userInterestEntity, oldCategoryId, newCategoryId);

            if (!isUpdated)
            {
                throw new Exception("Category not found");
            }

            return isUpdated;
        }
    }
}
