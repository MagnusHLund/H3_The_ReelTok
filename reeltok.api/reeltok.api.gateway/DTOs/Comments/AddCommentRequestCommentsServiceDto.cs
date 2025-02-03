using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentRequestCommentsServiceDto")]
    public class AddCommentRequestCommentsServiceDto
    {
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "VideoId")]
        public Guid VideoId { get; set; }
        [XmlElement(elementName: "CommentText")]
        public string CommentText { get; set; }

        public AddCommentRequestCommentsServiceDto(Guid userId, Guid videoId, string commentText)
        {
            UserId = userId;
            VideoId = videoId;
            CommentText = commentText;
        }
    }
}