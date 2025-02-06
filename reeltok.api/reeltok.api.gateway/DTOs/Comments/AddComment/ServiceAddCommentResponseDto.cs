using System.Xml.Serialization;
using reeltok.api.gateway.DTOs.Interfaces;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentResponseDto")]
    public class ServiceAddCommentResponseDto : BaseResponseDto, ICommentUsingUnixTimeDto
    {
        [XmlElement(elementName: "CommentId")]
        public Guid CommentId { get; set; }
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "CommentText")]
        [XmlElement("VideoId")]
        public Guid VideoId { get; set; }
        public string CommentText { get; set; }
        [XmlElement(elementName: "CreatedAt")]
        public uint CreatedAt { get; set; }

        public ServiceAddCommentResponseDto(Guid commentId, Guid userId, Guid videoId, string commentText, uint createdAt, bool success = true) : base(success)
        {
            CommentId = commentId;
            UserId = userId;
            VideoId = videoId;
            CommentText = commentText;
            CreatedAt = createdAt;
        }

        public ServiceAddCommentResponseDto() { }
    }
}