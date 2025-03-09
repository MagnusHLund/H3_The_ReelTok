using System.ComponentModel.DataAnnotations;

namespace reeltok.api.recommendations.Entities
{
    public class WatchedVideoEntity
    {
        [Key]
        public uint WatchedVideoId { get; set; }

        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }
        public ushort WatchCount { get; set; }
        public uint LastWatchedAt { get; set; }


        public WatchedVideoEntity(Guid userId, Guid videoId, ushort watchCount, uint lastWatchedAt)
        {
            UserId = userId;
            VideoId = videoId;
            WatchCount = watchCount;
            LastWatchedAt = lastWatchedAt;
        }

        private WatchedVideoEntity() { }
    }
}
