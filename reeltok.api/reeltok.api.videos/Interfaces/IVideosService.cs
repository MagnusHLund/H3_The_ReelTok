using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.videos.Entities;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Interfaces
{
    public interface IVideosService
    {
        Task<List<Video>> GetVideosForFeed(Guid userId, byte amount);
        Task<List<Video>> GetVideosForProfile(Guid userId, byte amountToReturn, uint amountReceived);
        Task<Video> UploadVideo(VideoUpload video);
        Task<bool> DeleteVideo(Guid userId, Guid videosId);
    }
}
