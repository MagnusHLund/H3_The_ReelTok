using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("LoadCommentsRequestCommentsServiceDto")]
    public class LoadCommentsRequestCommentsServiceDto
    {
        [XmlElement(elementName: "VideoId")]
        public Guid VideoId { get; set; }
        [XmlElement(elementName: "Amount")]
        public byte Amount { get; set; }

        public LoadCommentsRequestCommentsServiceDto(Guid videoId, byte amount)
        {
            VideoId = videoId;
            Amount = amount;
        }
    }
}