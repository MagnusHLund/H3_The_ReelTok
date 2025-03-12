using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public abstract class BaseCommentDetails<T>
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
        public abstract T CreatedAt { get; }

        protected BaseCommentDetails(Guid userId, Guid videoId, string message)
        {
            UserId = userId;
            VideoId = videoId;
            Message = message;
        }
    }
}
