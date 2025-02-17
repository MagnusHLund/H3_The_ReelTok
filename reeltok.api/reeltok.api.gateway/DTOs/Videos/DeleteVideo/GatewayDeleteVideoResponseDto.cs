using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.DeleteVideo
{
    [XmlRoot("DeleteVideoResponseDto")]
    public class GatewayDeleteVideoResponseDto : BaseResponseDto
    {
        public GatewayDeleteVideoResponseDto(bool success = true) : base(success) { }
    }
}
