using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("GetUserProfileDataRequestDto")]
    public class GatewayGetUserProfileDataRequestDto
    {
        [XmlElement("UserId")]
        public Guid UserId { get; set; }

        public GatewayGetUserProfileDataRequestDto(Guid userId)
        {
            UserId = userId;
        }
    }
}