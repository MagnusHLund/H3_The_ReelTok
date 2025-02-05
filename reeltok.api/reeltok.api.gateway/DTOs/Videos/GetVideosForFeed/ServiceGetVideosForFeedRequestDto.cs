using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.GetVideosForFeed
{
    [XmlRoot("GetVideosForFeedRequestDto")]
    public class ServiceGetVideosForFeedRequestDto
    {
        public Guid UserId { get; set; }
        public byte Amount { get; set; }

        public ServiceGetVideosForFeedRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}