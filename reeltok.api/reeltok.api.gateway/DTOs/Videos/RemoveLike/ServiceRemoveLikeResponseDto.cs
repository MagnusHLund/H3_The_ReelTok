using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.RemoveLike
{
    [XmlRoot("RemoveLikeResponseDto")]
    public class ServiceRemoveLikeResponseDto : BaseResponseDto
    {
        public ServiceRemoveLikeResponseDto(bool success = true) : base(success) { }
        public ServiceRemoveLikeResponseDto() { }
    }
}
