using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("DeleteVideoRequestDto")]
    public class DeleteVideoRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }
        [XmlElement("VideoId")]
        public Guid VideoId { get; set; }

        public DeleteVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
