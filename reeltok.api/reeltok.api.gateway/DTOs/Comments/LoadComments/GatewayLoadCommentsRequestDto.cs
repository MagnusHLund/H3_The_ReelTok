using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("LoadCommentsRequestDto")]
    public class GatewayLoadCommentsRequestDto
    {
        [XmlElement(elementName: "VideoId")]
        public Guid VideoId { get; set; }
        [XmlElement(elementName: "Amount")]
        public byte Amount { get; set; }

        public GatewayLoadCommentsRequestDto(Guid videoId, byte amount)
        {
            VideoId = videoId;
            Amount = amount;
        }
    }
}