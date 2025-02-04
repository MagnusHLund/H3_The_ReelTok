using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Auth
{
    [XmlRoot("LogOutUserResponseDto")]
    public class GatewayLogOutUserResponseDto : BaseResponseDto
    {
        public GatewayLogOutUserResponseDto(bool success) : base(success) { }
        public GatewayLogOutUserResponseDto() { }
    }
}