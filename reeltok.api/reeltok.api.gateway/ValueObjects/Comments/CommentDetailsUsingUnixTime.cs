using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public class CommentDetailsUsingUnixTime : AbstractCreatedAtType<uint>
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; }

        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; }

        [Required]
        [JsonProperty("Message")]
        public string Message { get; }

        [Required]
        [JsonProperty("CreatedAt")]
        public override uint CreatedAt { get; }

        public CommentDetailsUsingUnixTime(
            Guid userId,
            Guid videoId,
            string message,
            uint createdAt
        )
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
            CreatedAt = createdAt;
        }
    }
}
