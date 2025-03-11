using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class ServiceAddCommentRequestDto
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid VideoId { get; set; }

        [Required]
        [Range(1, 1024)]
        public string CommentText { get; set; }

        public ServiceAddCommentRequestDto(Guid userId, Guid videoId, string commentText)
        {
            UserId = userId;
            VideoId = videoId;
            CommentText = commentText;
        }
    }
}
