using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("AddCommentRequestDto")]
    public class ServiceAddCommentRequestDto
    {
        [XmlElement(elementName: "UserId")]
        [Required]
        public Guid UserId { get; set; }
        [XmlElement(elementName: "VideoId")]
        [Required]
        public Guid VideoId { get; set; }
        [XmlElement(elementName: "CommentText")]
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