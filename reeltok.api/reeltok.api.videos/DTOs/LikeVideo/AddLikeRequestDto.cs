using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.LikeVideo
{
    [XmlRoot("AddLikeRequestDto")]
    public class AddLikeRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("VideoId")]
        public Guid VideoId { get; set; }

        public AddLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }

        public AddLikeRequestDto() {}
    }
}
