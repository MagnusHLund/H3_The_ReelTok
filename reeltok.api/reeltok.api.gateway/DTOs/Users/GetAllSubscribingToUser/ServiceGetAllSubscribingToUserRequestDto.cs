using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscribingToUserRequestDto")]
    public class ServiceGetAllSubscribingToUserRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }

        public ServiceGetAllSubscribingToUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}