using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscriptionsForUserRequestDto")]
    public class GatewayGetAllSubscriptionsForUserRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }

        public GatewayGetAllSubscriptionsForUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}