using System.ComponentModel.DataAnnotations;
using reeltok.api.recommendations.ValueObjects;

namespace reeltok.api.recommendations.Entities
{
    public class WatchedVideoEntity
    {
        [Key]
        public uint WatchedVideoId { get; set; }

        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public ushort TimesWatched { get; set; }
        public uint LastWatchedAt { get; set; }


        public WatchedVideoEntity(Guid userId, Guid videoId, ushort timesWatched, uint lastWatchedAt)
        {
            UserId = userId;
            VideoId = videoId;
            TimesWatched = timesWatched;
            LastWatchedAt = lastWatchedAt;
        }

        private WatchedVideoEntity()
        {

        }
    }
}
