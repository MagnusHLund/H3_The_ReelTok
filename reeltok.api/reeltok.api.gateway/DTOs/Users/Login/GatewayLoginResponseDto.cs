using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("LoginResponseDto")]
    public class GatewayLoginResponseDto : BaseResponseDto
    {
        public GatewayLoginResponseDto(bool success) : base(success) { }
        public GatewayLoginResponseDto() { }
    }
}