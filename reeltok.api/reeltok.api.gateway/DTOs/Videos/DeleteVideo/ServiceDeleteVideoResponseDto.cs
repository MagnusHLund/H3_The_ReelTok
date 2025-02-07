
using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Videos.DeleteVideo
{
    [XmlRoot("DeleteVideoResponseDto")]
    public class ServiceDeleteVideoResponseDto : BaseResponseDto
    {
        public ServiceDeleteVideoResponseDto(bool success = true) : base(success) { }
        public ServiceDeleteVideoResponseDto() { }
    }
}