using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Auth
{
    [XmlRoot("LogOutUserResponseDto")]
    public class LogOutUserResponseDto : BaseResponseDto
    {
        public LogOutUserResponseDto()
        {
        }
        public LogOutUserResponseDto(bool success) : base(success)
        {
        }
    }
}