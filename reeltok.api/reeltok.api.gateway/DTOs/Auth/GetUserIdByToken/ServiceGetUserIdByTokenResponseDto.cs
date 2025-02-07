using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Auth
{
    [XmlRoot("GetUserIdByTokenResponseDto")]
    public class ServiceGetUserIdByTokenResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        public ServiceGetUserIdByTokenResponseDto(Guid userId, bool success = true) : base(success)
        {
            UserId = userId;
        }
        public ServiceGetUserIdByTokenResponseDto() { }
    }
}