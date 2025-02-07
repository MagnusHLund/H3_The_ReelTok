using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Comments
{
    [XmlRoot("LoadCommentsRequestDto")]
    public class ServiceLoadCommentsRequestDto
    {
        [XmlElement(elementName: "VideoId")]
        [Required]
        public Guid VideoId { get; set; }
        [XmlElement(elementName: "Amount")]
        [Required]
        public byte Amount { get; set; }

        public ServiceLoadCommentsRequestDto(Guid videoId, byte amount)
        {
            VideoId = videoId;
            Amount = amount;
        }
    }
}