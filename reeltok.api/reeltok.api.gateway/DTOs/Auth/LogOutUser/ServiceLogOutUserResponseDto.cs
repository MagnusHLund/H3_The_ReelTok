using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Auth
{
    [XmlRoot("LogOutUserResponseDto")]
    public class ServiceLogOutUserResponseDto : BaseResponseDto
    {
        public ServiceLogOutUserResponseDto(bool success = true) : base(success) { }
        public ServiceLogOutUserResponseDto() { }
    }
}
