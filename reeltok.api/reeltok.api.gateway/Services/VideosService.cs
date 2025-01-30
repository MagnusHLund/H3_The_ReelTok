using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces;

namespace reeltok.api.gateway.Services
{
    public class VideosService : IVideosService
    {
        private readonly IMapper _mapper;
        private readonly GatewayService _gatewayService;
        internal VideosService(IMapper mapper, GatewayService gatewayService)
        {
            _mapper = mapper;
            _gatewayService = gatewayService;
        }
        public void LikeVideo(Guid VideoId) { }
        public void RemoveLikeFromVideo(Guid VideoId) { }
        public List<Video> GetVideosForFeed(byte amount) { }
        public Stream GetVideoStream(Guid VideoId) { }
        public void UploadVideo(VideoUpload video) { }
        public VideoDetails EditVideoInformation(VideoDetails videoDetails) { }
        public void DeleteVideo(Guid videoId) { }
    }
}