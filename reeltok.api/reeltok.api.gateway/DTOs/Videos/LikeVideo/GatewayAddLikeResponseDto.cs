using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.LikeVideo
{
    [XmlRoot("AddLikeResponseDto")]
    public class GatewayAddLikeResponseDto : BaseResponseDto
    {
        public GatewayAddLikeResponseDto(bool success = true) : base(success) { }
    }
}
