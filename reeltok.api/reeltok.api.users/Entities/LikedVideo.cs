using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.users.ValueObjects;

namespace reeltok.api.users.Entities
{
    public class LikedVideo
    {
        public uint LikedId { get; } = 0;
        public LikedDetails Details { get; }
        public LikedVideo(uint likedId, LikedDetails details)
        {
            LikedId = likedId;
            Details = details;
        }
    }
}