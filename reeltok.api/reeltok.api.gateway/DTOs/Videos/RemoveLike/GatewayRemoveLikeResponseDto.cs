using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.RemoveLike
{
    [XmlRoot("RemoveLikeResponseDto")]
    public class GatewayRemoveLikeResponseDto : BaseResponseDto
    {
        public GatewayRemoveLikeResponseDto(bool success = true) : base(success) { }
    }
}