using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IVideoRecommendationAlgorithmRepository
    {
        Task<List<Guid>> GetTopVideoByUserInterestAsync(Guid userId, int amount);
    }
}
