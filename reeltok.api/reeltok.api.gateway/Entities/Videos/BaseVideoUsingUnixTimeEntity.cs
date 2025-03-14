using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.Entities.Videos
{
    public class BaseVideoUsingUnixTimeEntity : AbstractCreatedAtType<uint>
    {
        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; set; }

        [Required]
        [JsonProperty("StreamPath")]
        public string StreamPath { get; set; }

        [Required]
        [JsonProperty("UploadedAt")]
        public override uint CreatedAt { get; }

        protected BaseVideoUsingUnixTimeEntity(Guid videoId, string streamPath, uint createdAt) : base(createdAt)
        {
            VideoId = videoId;
            StreamPath = streamPath;
        }
    }
}
