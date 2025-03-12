using Newtonsoft.Json;

namespace reeltok.api.users.Entities
{
    public class HasUserLikedVideoEntity
    {
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [JsonProperty("HasUserLikedVideo")]
        public bool HasUserLikedVideo { get; set; }

        public HasUserLikedVideoEntity(Guid videoId, bool hasUserLikedVideo)
        {
            VideoId = videoId;
            HasUserLikedVideo = hasUserLikedVideo;
        }
    }
}
