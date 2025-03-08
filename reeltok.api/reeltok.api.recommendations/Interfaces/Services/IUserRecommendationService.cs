using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Entities;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IUsersService
    {
        Task<bool> AddRecommendationForUserAsync(UserInterestEntity userInterest, int categoryId);
        Task<UserInterestEntity> GetUserInterestAsync(Guid userId);
        Task<bool> UpdateRecommendationForUserAsync(Guid userId, int oldCategoryId, int newCategoryId);
    }
}
