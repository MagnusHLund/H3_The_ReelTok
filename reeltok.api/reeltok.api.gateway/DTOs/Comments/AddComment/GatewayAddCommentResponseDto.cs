using reeltok.api.gateway.Interfaces.DTOs;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class GatewayAddCommentResponseDto : BaseResponseDto, ICommentUsingDateTimeDto
    {
        [Required]
        public Guid CommentId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid VideoId { get; set; }

        [Required]
        [Range(1, 1024)]
        public string CommentText { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public GatewayAddCommentResponseDto(
            Guid commentId,
            Guid userId,
            Guid videoId,
            string commentText,
            DateTime createdAt,
            bool success = true
        ) : base(success)
        {
            CommentId = commentId;
            UserId = userId;
            VideoId = videoId;
            CommentText = commentText;
            CreatedAt = createdAt;
        }

        public GatewayAddCommentResponseDto() { }
    }
}
