using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.RemoveLike
{
    [XmlRoot("RemoveLikeRequestDto")]
    public class ServiceRemoveLikeRequestDto
    {
        public Guid UserId { get; set; }
        public Guid VideoId { get; set; }

        public ServiceRemoveLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}