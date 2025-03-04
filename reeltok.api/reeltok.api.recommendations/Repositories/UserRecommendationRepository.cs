using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class UserRecommendationRepository : IUserRecommendationRepository
    {
        public Task AddRecommendationForUserAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecommendationForUserAsync()
        {
            throw new NotImplementedException();
        }
    }
}
