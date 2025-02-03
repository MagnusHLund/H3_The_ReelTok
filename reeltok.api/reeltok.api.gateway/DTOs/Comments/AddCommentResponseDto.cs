using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("FailureResponseDto")]
    public class AddCommentResponseDto : BaseResponseDto
    {
        public Guid CommentId { get; set; }
        public Guid UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }

        public AddCommentResponseDto(Guid commentId, Guid userId, string commentText, DateTime createdAt, bool success) : base(success)
        {
            CommentId = commentId;
            UserId = userId;
            CommentText = commentText;
            CreatedAt = createdAt;
        }
    }
}