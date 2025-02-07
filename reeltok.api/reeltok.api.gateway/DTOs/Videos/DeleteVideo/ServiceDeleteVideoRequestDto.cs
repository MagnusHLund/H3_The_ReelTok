using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.DeleteVideo
{
    [XmlRoot("DeleteVideoRequestDto")]
    public class ServiceDeleteVideoRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("VideoId")]
        public Guid VideoId { get; set; }

        public ServiceDeleteVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}