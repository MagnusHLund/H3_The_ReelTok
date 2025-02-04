using System.Xml.Serialization;
using reeltok.api.gateway.Entities;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("LoadCommentsResponseDto")]
    public class GatewayLoadCommentsResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "Comments")]
        public List<CommentUsingDateTime> Comments { get; set; }

        public GatewayLoadCommentsResponseDto(List<CommentUsingDateTime> comments, bool success = true) : base(success)
        {
            Comments = comments;
        }
    }
}