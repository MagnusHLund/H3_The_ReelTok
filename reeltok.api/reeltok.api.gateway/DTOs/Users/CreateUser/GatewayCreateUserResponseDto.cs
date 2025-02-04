using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("CreateUserResponseDto")]
    public class GatewayCreateUserResponseDto : BaseResponseDto
    {
        public GatewayCreateUserResponseDto(bool success) : base(success) { }
        public GatewayCreateUserResponseDto() { }
    }
}