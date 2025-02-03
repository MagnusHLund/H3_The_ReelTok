using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("FailureResponseDto")]
    public class LoadCommentsResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Comments")]
        public List<CommentUsingDateTime> Comments { get; set; }

        public LoadCommentsResponseDto(List<CommentUsingDateTime> comments, bool success) : base(success)
        {
            Comments = comments;
        }
        public LoadCommentsResponseDto() { }
    }
}