using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.videos.Interfaces;

namespace reeltok.api.videos.Tests
{
    public class LikeServiceTests : ILikeService
    {
        public uint GetVideoLikesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> LikeVideo(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveLikeFromVideo(Guid userId, Guid videoId)
        {
            throw new NotImplementedException();
        }
    }
}
