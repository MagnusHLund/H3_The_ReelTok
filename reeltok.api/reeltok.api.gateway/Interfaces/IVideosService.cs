using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.Interfaces
{
    public interface IVideosService
    {
        public void LikeVideo(Guid VideoId);
        public void RemoveLikeFromVideo(Guid VideoId);
        public List<Video> GetVideosForFeed(byte amount);
        public Stream GetVideoStream(Guid VideoId);
        public void UploadVideo(VideoUpload video);
        public VideoDetails EditVideoInformation(VideoDetails videoDetails);
        public void DeleteVideo(Guid videoId);
    }
}