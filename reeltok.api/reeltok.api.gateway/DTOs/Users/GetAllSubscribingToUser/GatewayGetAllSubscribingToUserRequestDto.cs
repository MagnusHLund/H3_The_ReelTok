using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetAllSubscribingToUserRequestDto")]
    public class GatewayGetAllSubscribingToUserRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }

        public GatewayGetAllSubscribingToUserRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}