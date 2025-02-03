using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentResponseCommentsServiceDto")]
    public class AddCommentResponseCommentsServiceDto : BaseResponseDto
    {
        [XmlElement(elementName: "CommentId")]
        public Guid CommentId { get; set; }
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "CommentText")]
        public string CommentText { get; set; }
        [XmlElement(elementName: "CreatedAt")]
        public uint CreatedAt { get; set; }

        public AddCommentResponseCommentsServiceDto(Guid commentId, Guid userId, string commentText, uint createdAt, bool success) : base(success)
        {
            CommentId = commentId;
            UserId = userId;
            CommentText = commentText;
            CreatedAt = createdAt;
        }
    }
}