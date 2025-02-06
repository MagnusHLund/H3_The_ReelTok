using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs.RemoveLike
{
    [XmlRoot("RemoveLikeRequestDto")]
    internal class RemoveLikeRequestDto
    {
        [XmlElement("UserId")]
        internal Guid UserId { get; set; }
        [XmlElement("VideoId")]
        internal Guid VideoId { get; set; }

        internal RemoveLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}