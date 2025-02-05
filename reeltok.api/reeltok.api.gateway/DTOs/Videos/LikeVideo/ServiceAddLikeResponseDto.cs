using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.LikeVideo
{
    [XmlRoot("AddLikeResponseDto")]
    public class ServiceAddLikeResponseDto : BaseResponseDto
    {
        public ServiceAddLikeResponseDto(bool success = true) : base(success) { }
        public ServiceAddLikeResponseDto() { }
    }
}