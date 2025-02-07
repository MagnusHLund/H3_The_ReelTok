using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Services
{
    public class LikeService : ILikeService
    {
        private readonly IHttpService _httpService;
        public LikeService(IHttpService httpService) {
            _httpService = httpService;
        }

        public Task<bool> LikeVideo(Guid userId, Guid videoId)
        {
            // Calls users microservice
            throw new NotImplementedException();
        }

        public Task<bool> RemoveLikeFromVideo(Guid userId, Guid videoId)
        {
            // Calls users microservice
            throw new NotImplementedException();
        }
        public uint GetVideoLikesAsync()
        {
            // Calls users microservice
            throw new NotImplementedException();
        }
    }
}
