using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface IVideosService
    {
        public Task<bool> LikeVideo(Guid VideoId);
        public Task<bool> RemoveLikeFromVideo(Guid VideoId);
        public List<Video> GetVideosForFeed(byte amount);
        public Task<string> UploadVideo(VideoUpload video);
        public Task<bool> DeleteVideo(Guid videoId);
    }
}