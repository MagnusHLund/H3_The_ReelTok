using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reeltok.api.recommendations.ValueObjects
{
    public class WatchedVideoDetails
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public ushort TimeWatched { get; set; }


        public WatchedVideoDetails(Guid userId, Guid videoId, ushort timeWatched)
        {
            UserId = userId;
            VideoId = videoId;
            TimeWatched = timeWatched;

        }
    }
}