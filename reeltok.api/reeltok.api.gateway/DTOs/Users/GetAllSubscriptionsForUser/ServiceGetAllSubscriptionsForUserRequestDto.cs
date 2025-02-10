using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscriptionsForUserRequestDto")]
    public class ServiceGetAllSubscriptionsForUserRequestDto
    {
        [XmlElement("UserId")]
        [Required]
        public Guid UserId { get; set; }

        public ServiceGetAllSubscriptionsForUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}