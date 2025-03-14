using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.comments
{
    public class CommentUsingDateTime : BaseComment
    {
        [Required]
        [JsonProperty("CommentDetails")]
        public CommentDetailsUsingDateTime CommentDetails { get; set; }

        public CommentUsingDateTime(uint commentId, CommentDetailsUsingDateTime commentDetails) : base(commentId)
        {
            CommentId = commentId;
            CommentDetails = commentDetails;
        }
    }
}
