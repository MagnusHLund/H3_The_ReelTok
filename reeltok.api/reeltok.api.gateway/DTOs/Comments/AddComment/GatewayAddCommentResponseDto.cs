using Newtonsoft.Json;
using reeltok.api.gateway.Entities.comments;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class GatewayAddCommentResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Comment")]
        CommentUsingDateTime Comment { get; set; }

        public GatewayAddCommentResponseDto(
            CommentUsingDateTime comment, bool success = true) : base(success)
        {
            Comment = comment;
        }
    }
}
