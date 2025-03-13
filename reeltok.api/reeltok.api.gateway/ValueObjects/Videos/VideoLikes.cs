using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public class VideoLikes
    {
        [Required]
        [JsonProperty("TotalLikes")]
        public uint TotalLikes { get; }

        [Required]
        [JsonProperty("UserHasLikedVideo")]
        public bool UserHasLikedVideo { get; }

        public VideoLikes(uint totalLikes, bool userHasLikedVideo)
        {
            TotalLikes = totalLikes;
            UserHasLikedVideo = userHasLikedVideo;
        }
    }
}
