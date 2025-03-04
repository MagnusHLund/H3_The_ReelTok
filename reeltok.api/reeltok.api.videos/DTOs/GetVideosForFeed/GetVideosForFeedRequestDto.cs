using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("GetVideosForFeedRequestDto")]
    public class GetVideosForFeedRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("Amount")]
        public byte Amount { get; set; }

        public GetVideosForFeedRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}
