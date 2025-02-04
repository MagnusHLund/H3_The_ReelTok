using System.Xml.Serialization;

namespace reeltok.api.gateway.DTOs.Recommendations
{
    [XmlRoot("GetRecommendationsResponseDto")]
    public class ServiceGetRecommendationsResponseDto : BaseResponseDto
    {
        public ServiceGetRecommendationsResponseDto(bool success) : base(success) { }
        public ServiceGetRecommendationsResponseDto() { }
    }
}