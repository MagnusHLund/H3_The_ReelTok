using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("LoadCommentsResponseCommentsServiceDto")]
    public class LoadCommentsResponseCommentsServiceDto : BaseResponseDto
    {
        [XmlElement(elementName: "Comments")]
        public List<CommentUsingUnixTime> Comments { get; set; }

        public LoadCommentsResponseCommentsServiceDto(List<CommentUsingUnixTime> comments, bool success) : base(success)
        {
            Comments = comments;
        }
        public LoadCommentsResponseCommentsServiceDto() { }
    }
}