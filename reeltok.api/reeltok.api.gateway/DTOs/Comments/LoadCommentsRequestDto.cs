using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("FailureResponseDto")]
    public class LoadCommentsRequestDto
    {
        [XmlElement(elementName: "VideoId")]
        public Guid VideoId { get; set; }

        public LoadCommentsRequestDto(Guid videoId)
        {
            VideoId = videoId;
        }
    }
}