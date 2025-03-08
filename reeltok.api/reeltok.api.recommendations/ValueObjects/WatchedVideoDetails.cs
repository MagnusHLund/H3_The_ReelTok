namespace reeltok.api.recommendations.ValueObjects
{
    public class WatchedVideoDetails
    {
        public Guid UserId { get; private set; }
        public Guid VideoId { get; private set; }
        public ushort TimesWatched { get; private set; }
        public uint LastWatched { get; private set; }

        public WatchedVideoDetails(Guid userId, Guid videoId, ushort TimesWatched, uint lastWatched)
        {
            UserId = userId;
            VideoId = videoId;
            TimesWatched = TimesWatched;
            LastWatched = lastWatched;
        }

        public WatchedVideoDetails UpdateWatchedVideo()
        {
            // Get the current UTC time and convert to Unix timestamp as uint
            uint unixTimestamp = Convert.ToUInt32(Math.Floor(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds));

            // Create a new WatchedVideoDetails object with updated TimesWatched and LastWatched
            return new WatchedVideoDetails(UserId, VideoId, (ushort)(TimesWatched + 1), unixTimestamp);
        }

        private WatchedVideoDetails()
        {

        }
    }
}
