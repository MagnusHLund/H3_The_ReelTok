using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.RemoveLike
{

    [XmlRoot("RemoveLikeRequestDto")]
    public class ServiceRemoveLikeRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("VideoId")]
        public Guid VideoId { get; set; }

        public ServiceRemoveLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }

        public ServiceRemoveLikeRequestDto() { }
    }
}
