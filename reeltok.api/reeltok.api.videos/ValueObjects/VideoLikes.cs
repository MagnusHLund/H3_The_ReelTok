using Newtonsoft.Json;

namespace reeltok.api.videos.ValueObjects
{
    public class VideoLikes
    {
        [JsonProperty("TotalLikes")]
        public uint TotalLikes { get; }

        [JsonProperty("UserHasLikedVideo")]
        public bool UserHasLikedVideo { get; }

        public VideoLikes(uint totalLikes, bool userHasLikedVideo)
        {
            TotalLikes = totalLikes;
            UserHasLikedVideo = userHasLikedVideo;
        }
    }
}
