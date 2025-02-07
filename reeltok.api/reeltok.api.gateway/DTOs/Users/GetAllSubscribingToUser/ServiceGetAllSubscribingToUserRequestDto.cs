using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscribingToUserRequestDto")]
    public class ServiceGetAllSubscribingToUserRequestDto
    {
        [XmlElement("UserId")]
        [Required]
        public Guid UserId { get; set; }

        public ServiceGetAllSubscribingToUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}