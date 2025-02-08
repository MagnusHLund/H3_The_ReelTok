namespace reeltok.api.videos.ValueObjects
{
    public class VideoLikes
    {
        public uint TotalLikes { get; set; }
        public bool UserHasLikedVideo { get; set; }

        public VideoLikes (uint totalLikes, bool userHasLikedVideo) {
            TotalLikes = totalLikes;
            UserHasLikedVideo = userHasLikedVideo;
        }
    }
}
