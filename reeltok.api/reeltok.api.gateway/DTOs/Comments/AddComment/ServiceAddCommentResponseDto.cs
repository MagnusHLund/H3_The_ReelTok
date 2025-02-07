using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentResponseDto")]
    public class ServiceAddCommentResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "CommentId")]
        public Guid CommentId { get; set; }
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "CommentText")]
        [Range(1, 1024)]
        public string CommentText { get; set; }
        
        [XmlElement(elementName: "CreatedAt")]
        public uint CreatedAt { get; set; }

        public ServiceAddCommentResponseDto(Guid commentId, Guid userId, string commentText, uint createdAt, bool success = true) : base(success)
        {
            CommentId = commentId;
            UserId = userId;
            CommentText = commentText;
            CreatedAt = createdAt;
        }

        public ServiceAddCommentResponseDto() { }
    }
}