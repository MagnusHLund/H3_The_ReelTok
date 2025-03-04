using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.Interfaces.Repositories;

namespace reeltok.api.recommendations.Repositories
{
    public class VideoRecommendationRepository : IVideoRecommendationRepository
    {
        public Task AddRecommendationForVideoAsync()
        {
            throw new NotImplementedException();
        }
    }
}
