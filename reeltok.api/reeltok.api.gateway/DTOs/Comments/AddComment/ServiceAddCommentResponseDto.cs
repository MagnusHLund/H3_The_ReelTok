using Newtonsoft.Json;
using reeltok.api.gateway.Entities.comments;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class ServiceAddCommentResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Comment")]
        public CommentUsingUnixTime Comment { get; set; }

        public ServiceAddCommentResponseDto(CommentUsingUnixTime comment, bool success = true) : base(success)
        {
            Comment = comment;
        }
    }
}
