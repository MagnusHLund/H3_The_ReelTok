using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Comments.AddComment
{
    public class GatewayAddCommentRequestDto
    {
        [Required]
        public Guid VideoId { get; set; }

        [Required]
        [Range(1, 1024)]
        public string CommentText { get; set; }

        public GatewayAddCommentRequestDto(Guid videoId, string commentText)
        {
            VideoId = videoId;
            CommentText = commentText;
        }
    }
}
