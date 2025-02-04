using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Auth
{
    [XmlRoot("LogOutUserResponseDto")]
    public class ServiceLogOutUserResponseDto : BaseResponseDto
    {
        // TODO: Test if its an issue assigning a value to the only parameter, when also having a parameterless constructor.
        public ServiceLogOutUserResponseDto(bool success = true) : base(success) { }
        public ServiceLogOutUserResponseDto() { }
    }
}