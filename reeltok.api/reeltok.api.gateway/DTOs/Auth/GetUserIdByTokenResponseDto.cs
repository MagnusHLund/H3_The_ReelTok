using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Auth
{
    [XmlRoot("GetUserIdByTokeNResponseDto")]
    public class GetUserIdByTokenResponseDto : BaseResponseDto
    {
        [XmlElement(elementName: "UserId")]
        public Guid UserId { get; set; }
        public GetUserIdByTokenResponseDto(bool success, Guid userId) : base(success)
        {
            UserId = userId;
        }
        public GetUserIdByTokenResponseDto() { }
    }
}