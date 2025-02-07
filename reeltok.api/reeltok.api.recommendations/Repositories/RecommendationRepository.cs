using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Data;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
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

        public Task<List<RecommendedCategories>> GetRecommendationAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRecommendationAsync(Recommendations recommendations)
        {
            throw new NotImplementedException();
        }


    }
}