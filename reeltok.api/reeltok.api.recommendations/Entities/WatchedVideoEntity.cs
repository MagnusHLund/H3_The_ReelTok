using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class WatchedVideoEntity
    {
        [Key]
        public uint WatchedVideoId { get; private set; }

        public WatchedVideoDetails WatchedVideoDetails { get; private set; }

        public WatchedVideoEntity(WatchedVideoDetails watchedVideoDetails)
        {
            WatchedVideoDetails = watchedVideoDetails;
        }

        private WatchedVideoEntity()
        {

        }
    }
}
