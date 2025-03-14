using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.Entities.Videos
{
    public class BaseVideoUsingUnixTimeEntity
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("StreamPath")]
        public string StreamPath { get; set; }

        [Required]
        [JsonProperty("UploadedAt")]
        public long UploadedAt { get; set; }

        public BaseVideoUsingUnixTimeEntity(Guid videoId, string streamPath, long uploadedAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
            UploadedAt = uploadedAt;
        }
    }
}
