using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.RemoveLike
{
    [XmlRoot("RemoveLikeRequestDto")]
    public class ServiceRemoveLikeRequestDto
    {
        [XmlElement(elementName: "UserId")]
        [Required]
        public Guid UserId { get; set; }

        [XmlElement(elementName: "VideoId")]
        [Required]
        public Guid VideoId { get; set; }

        public ServiceRemoveLikeRequestDto(Guid userId, Guid videoId)
        {
            UserId = userId;
            VideoId = videoId;
        }
    }
}
