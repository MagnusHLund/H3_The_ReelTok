using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsResponseDto")]
    public class ServiceGetReResponseDto : BaseResponseDto
    {
        public ServiceGetReResponseDto(bool success) : base(success) { }
        public ServiceGetReResponseDto() { }
    }
}