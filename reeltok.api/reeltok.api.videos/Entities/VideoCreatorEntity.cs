using Newtonsoft.Json;
using reeltok.api.videos.ValueObjects;

namespace reeltok.api.videos.Entities
{
    public class VideoCreatorEntity : UserEntity
    {
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        public VideoCreatorEntity(
            Guid videoId,
            Guid userId,
            UserDetails userDetails
            ) : base(userId, userDetails)
        {
            VideoId = videoId;
        }
    }
}
