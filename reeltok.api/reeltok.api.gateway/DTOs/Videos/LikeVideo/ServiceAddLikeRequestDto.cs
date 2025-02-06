using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.LikeVideo
{
    [XmlRoot("AddLikeRequestDto")]
    public class ServiceAddLikeRequestDto
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public ServiceAddLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}