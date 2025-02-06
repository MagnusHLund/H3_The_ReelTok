using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.videos.Interfaces
{
    public interface IVideosService
    {
        Task<List<Video>> GetVideos(Guid userId, byte amount);
    }
}
