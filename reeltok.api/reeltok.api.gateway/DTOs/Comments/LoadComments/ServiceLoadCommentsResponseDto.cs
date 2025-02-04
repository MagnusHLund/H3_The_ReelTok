using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("LoadCommentsResponseDto")]
    public class ServiceLoadCommentsResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Comments")]
        public List<CommentUsingUnixTime> Comments { get; set; }

        public ServiceLoadCommentsResponseDto(List<CommentUsingUnixTime> comments, bool success = true) : base(success)
        {
            Comments = comments;
        }
        public ServiceLoadCommentsResponseDto() { }
    }
}