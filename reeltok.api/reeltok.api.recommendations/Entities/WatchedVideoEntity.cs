using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class WatchedVideoEntity
    {
        [Key]
        public uint WatchedVideoId { get; set; }

        public WatchedVideoDetails WatchedVideoDetails { get; set; }

        public WatchedVideoEntity(WatchedVideoDetails watchedVideoDetails)
        {
            WatchedVideoDetails = watchedVideoDetails;
        }

        public void IncrementTimesWatched()
        {
            WatchedVideoDetails = WatchedVideoDetails.UpdateWatchedVideo();
        }

        private WatchedVideoEntity()
        {

        }
    }
}
