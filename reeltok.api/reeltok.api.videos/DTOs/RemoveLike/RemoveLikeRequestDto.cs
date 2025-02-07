using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.RemoveLike
{
    [XmlRoot("RemoveLikeRequestDto")]
    public class RemoveLikeRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("VideoId")]
        public Guid VideoId { get; set; }

        public RemoveLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
