using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Interfaces;

namespace reeltok.api.recommendations.Repositories
{
    public class RecommendationRepository : IRecommendationsRepository
    {
        private readonly RecommendationDbContext _Context;

        public RecommendationRepository(RecommendationDbContext recommendationDbContext)
        {
            _Context = recommendationDbContext;
        }

        public Task<Recommendations> GetRecommendationAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecommendationAsync(Recommendations recommendation, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}