using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.gateway.ValueObjects;

namespace reeltok.api.gateway.Entities.comments
{
    public class CommentUsingUnixTime : BaseComment
    {
        [Required]
        [JsonProperty("CommentDetails")]
        public CommentDetailsUsingUnixTime CommentDetails { get; set; }

        public CommentUsingUnixTime(uint commentId, CommentDetailsUsingUnixTime commentDetails) : base(commentId)
        {
            CommentDetails = commentDetails;
        }
    }
}
