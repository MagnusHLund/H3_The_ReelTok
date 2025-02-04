using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentResponseDto")]
    public class GatewayAddCommentResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "CommentId")]
        public Guid CommentId { get; set; }
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "CommentText")]
        public string CommentText { get; set; }
        [XmlElement(elementName: "CreatedAt")]
        public DateTime CreatedAt { get; set; }

        public GatewayAddCommentResponseDto(Guid commentId, Guid userId, string commentText, DateTime createdAt, bool success = true) : base(success)
        {
            CommentId = commentId;
            UserId = userId;
            CommentText = commentText;
            CreatedAt = createdAt;
        }
    }
}