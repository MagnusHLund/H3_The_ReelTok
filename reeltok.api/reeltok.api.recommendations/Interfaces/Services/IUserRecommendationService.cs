using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.Interfaces.Services
{
    public interface IUserRecommendationService
    {
        Task AddRecommendationForUserAsync();
        Task UpdateRecommendationForUserAsync();
    }
}
