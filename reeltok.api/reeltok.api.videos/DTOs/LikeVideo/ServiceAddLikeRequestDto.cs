using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.LikeVideo
{
    [XmlRoot("AddLikeRequestDto")]
    public class ServiceAddLikeRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("VideoId")]
        public Guid VideoId { get; set; }

        public ServiceAddLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }

        public ServiceAddLikeRequestDto() {}
    }
}
