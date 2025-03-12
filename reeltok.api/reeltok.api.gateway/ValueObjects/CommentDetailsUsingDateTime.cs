using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.ValueObjects
{
    public class CommentDetailsUsingDateTime : BaseCommentDetails<DateTime>
    {
        [Required]
        [JsonProperty("CreatedAt")]
        public override DateTime CreatedAt { get; }

        public CommentDetailsUsingDateTime(
            Guid userId,
            Guid videoId,
            string message,
            DateTime createdAt
        ) : base(userId, videoId, message)
        {
            CreatedAt = createdAt;
        }
    }
}
