using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.Entities.Videos
{
    public class BaseVideoUsingDateTimeEntity : AbstractCreatedAtType<DateTime>
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("StreamPath")]
        public string StreamPath { get; set; }

        [Required]
        [JsonProperty("UploadedAt")]
        public override DateTime CreatedAt { get; }

        public BaseVideoUsingDateTimeEntity(Guid videoId, string streamPath, DateTime createdAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
            CreatedAt = createdAt;

        }
    }
}