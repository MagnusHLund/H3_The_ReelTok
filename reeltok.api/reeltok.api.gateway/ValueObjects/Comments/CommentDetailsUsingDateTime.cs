using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public class CommentDetailsUsingDateTime : AbstractCreatedAtType<DateTime>
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
        public override DateTime CreatedAt { get; }

        public CommentDetailsUsingDateTime(
            Guid userId,
            Guid videoId,
            string message,
            DateTime createdAt
        )
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
            CreatedAt = createdAt;
        }
    }
}
