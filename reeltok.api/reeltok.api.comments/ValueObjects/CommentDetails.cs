using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.comments.ValueObjects
{
    public class CommentDetails
    {
        [Required]
        [JsonProperty("UserId")]
        public Guid UserId { get; private set; } = Guid.Empty;

        [Required]
        [JsonProperty("VideoId")]
        public Guid VideoId { get; private set; } = Guid.Empty;

        [Required]
        [JsonProperty("Message")]
        public string Message { get; private set; } = string.Empty;

        [Required]
        [JsonProperty("CreatedAt")]
        public long CreatedAt { get; private set; } = 0;

        public CommentDetails(Guid userId, Guid videoId, string message, long createdAt)
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
            CreatedAt = createdAt;
        }

        private CommentDetails() { }
    }
}
