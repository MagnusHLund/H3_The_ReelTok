using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.Entities.Videos
{
    public class BaseVideoUsingDateTimeEntity
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("StreamPath")]
        public string StreamPath { get; set; }

        [Required]
        [JsonProperty("UploadedAt")]
        public DateTime CreatedAt { get; }

        public BaseVideoUsingDateTimeEntity(
            Guid videoId,
            string streamPath,
            DateTime createdAt
        )
        {
            VideoId = videoId;
            StreamPath = streamPath;
            CreatedAt = createdAt;
        }
    }
}