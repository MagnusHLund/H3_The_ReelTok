using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("LoginResponseDto")]
    public class ServiceLoginResponseDto : BaseResponseDto
    {
        public ServiceLoginResponseDto(bool success) : base(success) { }
        public ServiceLoginResponseDto() { }
    }
}