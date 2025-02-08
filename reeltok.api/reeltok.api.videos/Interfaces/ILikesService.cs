using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Interfaces
{
    public interface ILikesService
    {
        Task<bool> LikeVideo(Guid userId, Guid videoId);
        Task<bool> RemoveLikeFromVideo(Guid userId, Guid videoId);
        Task<VideoLikes> GetVideoLikes(Guid userId, Guid videoId);
    }
}
