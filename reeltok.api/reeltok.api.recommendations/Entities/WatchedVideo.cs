using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class WatchedVideo
    {
        public uint WatchedVideoId { get; set; }

        public WatchedVideoDetails WatchedVideoDetails{ get; set; }

        public WatchedVideo(uint watchedVideoId, WatchedVideoDetails watchedVideoDetails)
        {
            WatchedVideoId = watchedVideoId;
            WatchedVideoDetails = watchedVideoDetails;
        }
    }
}