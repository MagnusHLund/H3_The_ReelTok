using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace reeltok.api.gateway.Entities.comments
{
    public abstract class BaseComment
    {
        [Required]
        [JsonProperty("CommentId")]
        public uint CommentId { get; set; }

        protected BaseComment(uint commentId)
        {
            CommentId = commentId;
        }
    }
}
