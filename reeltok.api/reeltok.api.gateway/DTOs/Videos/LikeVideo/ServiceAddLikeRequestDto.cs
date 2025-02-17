using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

namespace reeltok.api.gateway.DTOs.Videos.LikeVideo
{
    [XmlRoot("AddLikeRequestDto")]
    public class ServiceAddLikeRequestDto
    {

        [XmlElement(elementName: "UserId")]
        [Required]
        public Guid UserId { get; set; }

        [XmlElement(elementName: "VideoId")]
        [Required]
        public Guid VideoId { get; set; }

        public ServiceAddLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
