using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("FailureResponseDto")]
    public class LoadCommentsRequestCommentsServiceDto
    {
        [XmlElement(elementName: "VideoId")]
        public Guid VideoId { get; set; }

        public LoadCommentsRequestCommentsServiceDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}