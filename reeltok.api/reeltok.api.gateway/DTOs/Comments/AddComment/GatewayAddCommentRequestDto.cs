using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentRequestDto")]
    public class GatewayAddCommentRequestDto
    {
        [XmlElement(elementName: "VideoId")]
        [Required]
        public Guid VideoId { get; set; }
        [XmlElement(elementName: "CommentText")]
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