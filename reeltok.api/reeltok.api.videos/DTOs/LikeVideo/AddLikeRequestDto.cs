using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.LikeVideo
{
    [XmlRoot("AddLikeRequestDto")]
    internal class AddLikeRequestDto
    {
        [XmlElement("UserId")]
        internal Guid UserId { get; set; }
        [XmlElement("VideoId")]
        internal Guid VideoId { get; set; }

        internal AddLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}