using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Entities;
using reeltok.api.recommendations.Enums;
using reeltok.api.recommendations.Interfaces;

namespace reeltok.api.recommendations.Services
{
    public class RecommendationsService : IRecommendationsService
    {
        private readonly IRecommendationsRepository _recommendationsRepository;
        public RecommendationsService(IRecommendationsRepository recommendationsRepository)
        {
            _recommendationsRepository = recommendationsRepository;
        }
        public Task<List<RecommendationsEnum>> GetRecommendation(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRecommendation()
        {
            throw new NotImplementedException();
        }
    }
}