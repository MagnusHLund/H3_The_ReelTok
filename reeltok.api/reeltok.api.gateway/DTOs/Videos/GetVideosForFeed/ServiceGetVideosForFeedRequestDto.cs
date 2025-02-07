using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    [XmlRoot("GetVideosForFeedRequestDto")]
    public class ServiceGetVideosForFeedRequestDto
    {
        
        [XmlElement("UserId")]
        [Required]
        public Guid UserId { get; set; }

        [XmlElement("Amount")]
        [Required]
        public byte Amount { get; set; }

        public ServiceGetVideosForFeedRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}