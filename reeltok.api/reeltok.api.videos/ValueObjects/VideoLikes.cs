namespace reeltok.api.videos.ValueObjects
{
    public class VideoLikes
    {
        public uint TotalLikes { get; }
        public bool UserHasLikedVideo { get; }

        public VideoLikes(uint totalLikes, bool userHasLikedVideo)
        {
            TotalLikes = totalLikes;
            UserHasLikedVideo = userHasLikedVideo;
        }
    }
}
