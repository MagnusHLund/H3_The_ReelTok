using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.Entities.Videos
{
    public class BaseVideoEntity
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("StreamPath")]
        public string StreamPath { get; set; }

        [Required]
        [JsonProperty("UploadedAt")]
        public uint UploadedAt { get; set; }

        protected BaseVideoEntity(Guid videoId, string streamPath, uint uploadedAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
            UploadedAt = uploadedAt;
        }
    }
}
