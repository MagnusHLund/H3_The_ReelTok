using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.gateway.Interfaces
{
    public interface IRecommendationsService
    {
        public Task<bool> ChangeRecommendedCategory(string category);
    }
}