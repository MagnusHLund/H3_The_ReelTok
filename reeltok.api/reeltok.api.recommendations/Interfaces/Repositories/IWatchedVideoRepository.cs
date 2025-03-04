using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.Interfaces.Repositories
{
    public interface IWatchedVideoRepository
    {
        Task AddWatchedVideoAsync();
        Task UpdateWatchedVideoAsync();
    }
}
