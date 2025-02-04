using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentRequestDto")]
    public class GatewayAddCommentRequestDto
    {
        [XmlElement(elementName: "VideoId")]
        public Guid VideoId { get; set; }
        [XmlElement(elementName: "CommentText")]
        public string CommentText { get; set; }

        public GatewayAddCommentRequestDto(Guid videoId, string commentText)
        {
            VideoId = videoId;
            CommentText = commentText;
        }
    }
}