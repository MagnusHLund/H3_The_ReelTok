using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.videos.Interfaces
{
    public interface ILikeService
    {
        Task<bool> LikeVideo(Guid userId, Guid videoId);
        Task<bool> RemoveLikeFromVideo(Guid userId, Guid videoId);
        uint GetVideoLikesAsync();
    }
}
