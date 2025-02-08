using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.DeleteVideo
{
    [XmlRoot("DeleteVideoRequestDto")]
    public class ServiceDeleteVideoRequestDto
    {
        [XmlElement("UserId")]
        [Required]
        public Guid UserId { get; set; }
        [XmlElement("VideoId")]
        [Required]
        public Guid VideoId { get; set; }

        public ServiceDeleteVideoRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}