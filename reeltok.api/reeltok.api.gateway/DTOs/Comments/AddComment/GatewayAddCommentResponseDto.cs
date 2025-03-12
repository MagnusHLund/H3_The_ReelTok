using reeltok.api.gateway.Entities;
using reeltok.api.gateway.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class GatewayAddCommentResponseDto : BaseResponseDto
    {
        [Required]
        CommentUsingDateTime Comment { get; set; }

        public GatewayAddCommentResponseDto(
            CommentUsingDateTime comment, bool success = true) : base(success)
        {
            Comment = comment;
        }
    }
}
