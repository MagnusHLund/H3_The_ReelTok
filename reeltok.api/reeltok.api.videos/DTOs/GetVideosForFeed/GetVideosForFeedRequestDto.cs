using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("GetVideosForFeedRequestDto")]
    internal class GetVideosForFeedRequestDto
    {
        [XmlElement("UserId")]
        internal Guid UserId { get; set; }
        [XmlElement("Amount")]
        internal byte Amount { get; set; }

        internal GetVideosForFeedRequestDto(Guid userId, byte amount)
        {
            UserId = userId;
            Amount = amount;
        }
    }
}