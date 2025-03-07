namespace reeltok.api.recommendations.ValueObjects
{
    public class WatchedVideoDetails
    {
        public Guid UserId { get; private set; }
        public Guid VideoId { get; private set; }
        public ushort TimeWatched { get; private set; }
        public uint LastWatched { get; private set; }

        public WatchedVideoDetails(Guid userId, Guid videoId, ushort timeWatched, uint lastWatched)
        {
            UserId = userId;
            VideoId = videoId;
            TimeWatched = timeWatched;
            LastWatched = lastWatched;
        }

        public WatchedVideoDetails UpdateWatchedVideo()
        {
            // Get the current UTC time and convert to Unix timestamp as uint
            uint unixTimestamp = Convert.ToUInt32(Math.Floor(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds));

            // Create a new WatchedVideoDetails object with updated TimeWatched and LastWatched
            return new WatchedVideoDetails(UserId, VideoId, (ushort) (TimeWatched + 1), unixTimestamp);
        }

        private WatchedVideoDetails()
        {

        }
    }
}
