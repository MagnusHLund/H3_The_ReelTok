using System.Xml.Serialization;

namespace reeltok.api.videos.DTOs
{
    [XmlRoot("DeleteVideoRequestDto")]
    internal class DeleteVideoRequestDto
    {
        [XmlElement("UserId")]
        internal Guid UserId { get; set; }
        [XmlElement("VideoId")]
        internal Guid VideoId { get; set; }

        internal DeleteVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}