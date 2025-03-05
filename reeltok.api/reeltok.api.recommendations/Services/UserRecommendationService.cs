using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces.Repositories;
using reeltok.api.recommendations.Interfaces.Services;

namespace reeltok.api.recommendations.Services
{
    public class UserRecommendationService : IUserRecommendationService
    {
        private readonly IUserRecommendationRepository _userRecommendationRepository;

        public UserRecommendationService(IUserRecommendationRepository userRecommendationRepository)
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

        public Task<bool> UpdateRecommendationForUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
