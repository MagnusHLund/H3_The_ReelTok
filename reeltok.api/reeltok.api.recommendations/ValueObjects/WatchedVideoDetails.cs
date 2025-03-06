namespace reeltok.api.recommendations.ValueObjects
{
    public class WatchedVideoDetails
    {
        public Guid UserId { get; private set; }
        public Guid VideoId { get; private set; }
        public ushort TimeWatched { get; private set; }

        public WatchedVideoDetails(Guid userId, Guid videoId, ushort timeWatched)
        {
            UserId = userId;
            VideoId = videoId;
            TimeWatched = timeWatched;
        }
        public WatchedVideoDetails IncrementTimeWatched()
        {
            return new WatchedVideoDetails(UserId, VideoId, (ushort) (TimeWatched + 1));
        }

        private WatchedVideoDetails()
        {

        }
    }
}
