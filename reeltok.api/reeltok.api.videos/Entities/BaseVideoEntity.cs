using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.videos.Entities
{
    public class BaseVideoEntity
    {
        [Key]
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("StreamPath")]
        public string StreamPath { get; set; }

        [Required]
        [JsonProperty("UploadedAt")]
        public long UploadedAt { get; set; }

        public BaseVideoEntity(Guid videoId, string streamPath, long uploadedAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
            UploadedAt = uploadedAt;
        }
    }
}