using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IUserRecommendationRepository
    {
        Task AddRecommendationForUserAsync(UserInterestEntity userInterest, int categoryId);
        Task UpdateRecommendationForUserAsync(Guid userId, RecommendedCategories updateCategory);
    }
}
