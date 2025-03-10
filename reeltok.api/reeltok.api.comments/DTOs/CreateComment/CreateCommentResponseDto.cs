using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using reeltok.api.comments.Entities;

namespace reeltok.api.comments.DTOs.CreateComment
{
    public class CreateCommentResponseDto : BaseResponseDto
    {
        [Required]
        [JsonProperty("Comment")]
        public CommentEntity Comment { get; set; }

        public CreateCommentResponseDto(CommentEntity comment, bool success = true) : base(success)
        {
            Comment = comment;
        }
    }
}