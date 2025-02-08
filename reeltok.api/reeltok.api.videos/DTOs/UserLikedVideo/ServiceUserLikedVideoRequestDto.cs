using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.UserLikedVideo
{
    [XmlRoot("UserLikedVideoRequestDto")]
    public class ServiceUserLikedVideoRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public ServiceUserLikedVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
