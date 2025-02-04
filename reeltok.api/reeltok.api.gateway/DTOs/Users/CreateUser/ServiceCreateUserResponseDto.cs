using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Users
{
    [XmlRoot("CreateUserResponseDto")]
    public class ServiceCreateUserResponseDto : BaseResponseDto
    {
        public ServiceCreateUserResponseDto(bool success) : base(success) { }
        public ServiceCreateUserResponseDto() { }
    }
}