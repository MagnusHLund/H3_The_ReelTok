using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("UpdateUserDetailsResponseDto")]
    public class GatewayUpdateUserDetailsResponseDto : BaseResponseDto
    {
        public GatewayUpdateUserDetailsResponseDto(bool success) : base(success) { }
        public GatewayUpdateUserDetailsResponseDto() { }
    }
}